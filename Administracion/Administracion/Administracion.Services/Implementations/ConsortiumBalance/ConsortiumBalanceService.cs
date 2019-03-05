using Administracion.DomainModel;
using Administracion.Services.Contracts.ConsortiumBalance;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using NVelocity;
using NVelocity.App;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Administracion.Services.Implementations.ConsortiumBalance
{
    public class ConsortiumBalanceService : IConsortiumBalanceService
    {
        public byte[] GetPDFBalance(ConsortiumBalanceStruct consortiumBalanceStruct)
        {
            Byte[] resultPDF;

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(consortiumBalanceStruct.HtmlBalanceStyles.ToString())))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(consortiumBalanceStruct.HtmlBalance.ToString())))
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

        public ConsortiumBalanceStruct GetBalance(Consortium consortium, IList<UnitAccountStatusSummary> balances, int month)
        {
            Velocity.Init();

            List<ConsortiumBalanceItem> velocityBalances = new List<ConsortiumBalanceItem>();

            foreach (var item in balances)
            {
                item.Expensas = item.GastosA + item.GastosB;
                item.GastosAPercent = GetExpensePencentage(balances.Sum(x => x.GastosA), item.GastosA);
                item.GastosBPercent = GetExpensePencentage(balances.Sum(x => x.GastosB), item.GastosB);

                velocityBalances.Add(new ConsortiumBalanceItem
                {
                    Uf = item.Uf,
                    Piso = item.Piso,
                    Dto = item.Dto,
                    Propietario = item.Propietario,
                    SaldoAnterior = item.SaldoAnterior.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Pagos = item.Pagos.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Deuda = item.MesActual.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Intereses = item.Intereses.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    GastosA = item.GastosA.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    GastosB = item.GastosB.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Expensas = item.Expensas.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Aysa = item.Aysa.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Edesur = item.Edesur.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    SiPagaAntes = item.SiPagaAntes.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    Total = item.Total.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                    DiscountValue = item.DiscountValue != null ? ((decimal)item.DiscountValue).ToString("$#,###,##0.00", new CultureInfo("es-AR")) : "",
                    GastosAPercent = item.GastosAPercent.ToString("#,###,##0.00", new CultureInfo("es-AR")),
                    GastosBPercent = item.GastosBPercent.ToString("#,###,##0.00", new CultureInfo("es-AR")),
                });
            }

            var fromDate = new DateTime(DateTime.Now.Year, month, 1, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, month, fromDate.AddMonths(1).AddDays(-1).Day, 0, 0, 0);

            var model = new
            {
                FromDate = fromDate.ToString("MMM dd", new CultureInfo("es-AR")).ToUpper(),
                ToDate = toDate.ToString("MMM dd", new CultureInfo("es-AR")).ToUpper(),
                Month = fromDate.ToString("MMMM", new CultureInfo("es-AR")),
                Year = fromDate.Year,
                Consortium = consortium,
                Balances = velocityBalances,
                TotalSaldoAnterior = balances.Sum(x=> x.SaldoAnterior).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalPagos = balances.Sum(x => x.Pagos).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalDeuda = balances.Sum(x => x.MesActual).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalIntereses = balances.Sum(x => x.Intereses).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalGastosA = balances.Sum(x => x.GastosA).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalGastosB = balances.Sum(x => x.GastosB).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalExpensas = balances.Sum(x => x.Expensas).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalAysa = balances.Sum(x => x.Aysa).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalEdesur = balances.Sum(x => x.Edesur).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalSiPagaAntes = balances.Sum(x => x.SiPagaAntes).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalTotal = balances.Sum(x => x.Total).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                DiscountDay = balances.First().DiscountDay ?? 0,
                TotalGastosAPercent = balances.Sum(x => x.GastosAPercent).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                TotalGastosBPercent = balances.Sum(x => x.GastosBPercent).ToString("#,###,##0.00", new CultureInfo("es-AR"))
            };

            var velocityContext = new VelocityContext();
            velocityContext.Put("model", model);

            var templateFile = string.Format(@"{0}\Resources\ConsortiumBalanceTemplate.html", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var template = new StringBuilder(File.ReadAllText(templateFile));
            var stylesFile = string.Format(@"{0}\Resources\ConsortiumBalance.css", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var styles = new StringBuilder(File.ReadAllText(stylesFile));


            var finalTickets = new StringBuilder();
            Velocity.Evaluate(velocityContext, new StringWriter(finalTickets), "Payment Tickets", new StringReader(template.ToString()));

            return new ConsortiumBalanceStruct { HtmlBalance = finalTickets, HtmlBalanceStyles = styles };
        }
        

        private decimal GetExpensePencentage(decimal total, decimal expense)
        {
            return expense != 0 ? (expense * 100) / total : 0;
        }
    }

}
