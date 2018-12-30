using Administracion.DomainModel;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Implementations.PaymentTickets;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using NVelocity;
using NVelocity.App;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Administracion.Services.Implementations.Consortiums
{
    public class PaymentTicketService : IPaymentTicketService
    {
        public byte[] GetPDFTickets(PaymentTicketsStruct paymentTicketsStruct)
        {
            Byte[] resultPDF;

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(paymentTicketsStruct.HtmlTicketsStyles.ToString())))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(paymentTicketsStruct.HtmlTickets.ToString())))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }


                        doc.Close();
                    }
                }
                resultPDF = ms.ToArray();
            }

            return resultPDF;
        }

        public PaymentTicketsStruct GetTickets(Consortium consortium, IList<PaymentTicket> paymentTickets, int month)
        {
            Velocity.Init();
            var toDate = month < 12 ? new DateTime(DateTime.Now.Year, month + 1, 18, 0, 0, 0).ToString("MMM dd", new CultureInfo("es-AR")).ToUpper() :
                new DateTime(DateTime.Now.Year + 1, 1, 18, 0, 0, 0).ToString("MMM dd", new CultureInfo("es-AR")).ToUpper();

            var model = new
            {
                FromDate = new DateTime(DateTime.Now.Year, month, 18, 0, 0, 0).ToString("MMM dd", new CultureInfo("es-AR")).ToUpper(),
                ToDate = toDate,
                ConsortiumAddress = consortium.Ownership.Address.Street + " " + consortium.Ownership.Address.Number,
                Items = paymentTickets,
                FontFamily = "Arial",
                FontSize = "12pt"
            };
            var velocityContext = new VelocityContext();
            velocityContext.Put("model", model);

            var templateFile = string.Format(@"{0}\Resources\PaymentTicketsTemplate.html", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var template = new StringBuilder(File.ReadAllText(templateFile));
            var stylesFile = string.Format(@"{0}\Resources\PaymentTickets.css", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var styles = new StringBuilder(File.ReadAllText(stylesFile));


            var finalTickets = new StringBuilder();
            Velocity.Evaluate(velocityContext, new StringWriter(finalTickets), "Payment Tickets", new StringReader(template.ToString()));

            return new PaymentTicketsStruct { HtmlTickets = finalTickets, HtmlTicketsStyles = styles };
        }

    }
}
