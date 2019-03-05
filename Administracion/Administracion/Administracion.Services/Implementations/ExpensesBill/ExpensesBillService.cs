using Administracion.DomainModel;
using Administracion.Services.Contracts.ExpensesBill;
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
using System.Linq;
using System.Reflection;
using System.Text;

namespace Administracion.Services.Implementations.ExpensesBill
{
    public class ExpensesBillService : IExpensesBillervice
    {
        public byte[] GetPDFTickets(ExpensesBillStruct expensesBillStruct)
        {
            Byte[] resultPDF;

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(expensesBillStruct.HtmlExpensesStyles.ToString())))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(expensesBillStruct.HtmlExpenses.ToString())))
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

        public ExpensesBillStruct GetExpensesBill(Consortium consortium, IList<Spend> expenses, IList<UnitAccountStatusSummary> unitsReport, int month)
        {
            Velocity.Init();

            foreach (var item in expenses)
            {
                item.Bill.FormatedAmount = item.Bill.Amount.ToString("$#,###,##0.00", new CultureInfo("es-AR"));
            }


            var salaryItems = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("detalle de sueldo y cargas sociales")).ToList();
            decimal salaryTotal = 0;
            var spendItemDetYSueldos = new List<Spend>();
            foreach (var item in salaryItems) {
                if (item.Description != "Suterh" && item.Description != "Fateryh" && item.Description != "Seracarh")
                {
                    if (item.Bill.Amount != 0)
                    {
                        salaryTotal += item.SpendClass.Id == 5 ? item.Bill.Amount * -1 : item.Bill.Amount;
                    }

                    spendItemDetYSueldos.Add(item);
                }
            }

            //var spendItemAportesYContr = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("aportes y contribuciones")).ToList();
            var spendItemAportesYContr = new List<Spend>();

            salaryItems.ForEach(x =>
            {
                if (x.Description == "Suterh" || x.Description == "Fateryh" || x.Description == "Seracarh")
                {
                    spendItemAportesYContr.Add(x);
                }
            });
        
            var contributionsTotal = spendItemAportesYContr.Sum(x => x.Bill.Amount);

            var salaryContributionsTotal = salaryTotal + spendItemAportesYContr.Sum(x => x.Bill.Amount);

            var spendItemServPub = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("servicios públicos")).ToList();
            foreach (var item in spendItemServPub)
            {
                item.Bill.SimpleExpirationDate = item.Bill.ExpirationDate.ToString("dd/mm/yyyy");
            }
            var publicServicesTotal = spendItemServPub.Sum(x => x.Bill.Amount);

            var spendItemAbonoServ = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("abono de servicios")).ToList();
            var servicePaymentTotal = spendItemAbonoServ.Sum(x => x.Bill.Amount);

            var spendItemMantPartesComunes = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("mantenimiento de partes comunes")).ToList();
            var commonMaintenanceTotal = spendItemMantPartesComunes.Sum(x => x.Bill.Amount);

            var spendItemRepUnidades = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("trabajo de reparaciones en unidades")).ToList();
            var unitRepairsTotal = spendItemRepUnidades.Sum(x => x.Bill.Amount);

            var spendItemGastosBancarios = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("gastos bancarios")).ToList();
            var bankExpensesTotal = spendItemGastosBancarios.Sum(x => x.Bill.Amount);

            var spendItemGastosLimpieza = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("gastos de limpieza")).ToList();
            var cleaningExpensesTotal = spendItemGastosLimpieza.Sum(x => x.Bill.Amount);

            var spendItemGastosAdmin = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("gastos de administracion")).ToList();
            var adminExpensesTotal = spendItemGastosAdmin.Sum(x => x.Bill.Amount);

            var spendItemPagosSeguros = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("pagos del período de seguros")).ToList();
            var insuranceExpensesTotal = spendItemPagosSeguros.Sum(x => x.Bill.Amount);

            var spendItemOtros = expenses.Where(x => x.Type.Item.Description.ToLower().Equals("otros")).ToList();
            var otherExpensesTotal = spendItemOtros.Sum(x => x.Bill.Amount);

            var total = salaryContributionsTotal + publicServicesTotal + servicePaymentTotal + commonMaintenanceTotal + unitRepairsTotal +
                bankExpensesTotal + cleaningExpensesTotal + adminExpensesTotal + insuranceExpensesTotal + otherExpensesTotal;

            var fromDate = new DateTime(DateTime.Now.Year, month, 1, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, month, fromDate.AddMonths(1).AddDays(-1).Day, 0, 0, 0);

            var juicios = consortium.Juicios;

            var model = new
            {
                FromDate = fromDate.ToString("MMM dd", new CultureInfo("es-AR")).ToUpper(),
                ToDate = toDate.ToString("MMM dd", new CultureInfo("es-AR")).ToUpper(),
                Month = fromDate.ToString("MMMM", new CultureInfo("es-AR")),
                Year = fromDate.Year,
                ConsortiumSuther = consortium.ClaveSuterh,
                ConsortiumCUIT = consortium.CUIT,
                ConsortiumName = consortium.FriendlyName,
                ConsortiumAddress = consortium.Ownership.Address.Street + " " + consortium.Ownership.Address.Number,
                AdministrationName = consortium.Administration.Name,
                ConsortiumMail = consortium.MailingList,
                ConsortiumPhone = consortium.Telephone,
                AdministrationCUIT = consortium.Administration.CUIT,
                AdministrationAddress = consortium.Administration.Address.Street + " " + consortium.Administration.Address.Number,
                ManagerName = consortium.Managers.FirstOrDefault() != null ? consortium.Managers.FirstOrDefault().User.Name + " " + consortium.Managers.FirstOrDefault().User.Surname : "",
                ManagerCUIT = consortium.Managers.FirstOrDefault() != null ? consortium.Managers.FirstOrDefault().User.CUIT : "",
                ManagerAlternate = consortium.Managers.FirstOrDefault()?.IsAlternate,
                BuildingCategory = consortium.Ownership.Category,
                ManagerCategory = "1° Categoría",
                Salary = spendItemDetYSueldos,
                SalaryTotal = salaryTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                SalaryPercent = GetExpensepencentage(total, salaryTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                Contributions = spendItemAportesYContr,
                ContributionsTotal = contributionsTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                ContributionsPercent = GetExpensepencentage(total, contributionsTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                SalaryContributionsTotal = salaryContributionsTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                SalaryContributionsPercent = GetExpensepencentage(total, salaryContributionsTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                PublicServices = spendItemServPub,
                PublicServicesTotal = publicServicesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                PublicServicesPercent = GetExpensepencentage(total, publicServicesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                ServicePayment = spendItemAbonoServ,
                ServicePaymentTotal = servicePaymentTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                ServicePaymentPercent = GetExpensepencentage(total, servicePaymentTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                CommonMaintenance = spendItemMantPartesComunes,
                CommonMaintenanceTotal = commonMaintenanceTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                CommonMaintenancePercent = GetExpensepencentage(total, commonMaintenanceTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                UnitRepairs = spendItemRepUnidades,
                UnitRepairsTotal = unitRepairsTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                UnitRepairsPercent = GetExpensepencentage(total, unitRepairsTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                BankExpenses = spendItemGastosBancarios,
                BankExpensesTotal = bankExpensesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                BankExpensesPercent = GetExpensepencentage(total, bankExpensesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                CleaningExpenses = spendItemGastosLimpieza,
                CleaningExpensesTotal = cleaningExpensesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                CleaningExpensesPercent = GetExpensepencentage(total, cleaningExpensesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                AdminExpenses = spendItemGastosAdmin,
                AdminExpensesTotal = adminExpensesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                AdminExpensesPercent = GetExpensepencentage(total, adminExpensesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                InsuranceExpenses = spendItemPagosSeguros,
                InsuranceExpensesTotal = insuranceExpensesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                InsuranceExpensesPercent = GetExpensepencentage(total, insuranceExpensesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                OtherExpenses = spendItemOtros,
                OtherExpensesTotal = otherExpensesTotal.ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                OtherExpensesPercent = GetExpensepencentage(total, otherExpensesTotal).ToString("#,###,##0.00", new CultureInfo("es-AR")),
                TotalDebt = (unitsReport.Sum(x => x.SaldoAnterior) * - 1).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalInTermsPayments = unitsReport.Sum(x => x.Pagos).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                TotalInterest = unitsReport.Sum(x => x.Intereses).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                ClosingTotal = (unitsReport.Sum(x => x.Pagos) - unitsReport.Sum(x => x.MesActual) + (unitsReport.Sum(x => x.SaldoAnterior))).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                ChargeTotal = (unitsReport.Sum(x => x.Total) * -1).ToString("$#,###,##0.00", new CultureInfo("es-AR")),
                Juicios = juicios,
                Total = total.ToString("$#,###,##0.00", new CultureInfo("es-AR"))
            };

            var velocityContext = new VelocityContext();
            velocityContext.Put("model", model);

            var templateFile = string.Format(@"{0}\Resources\ConsortiumExpensesTemplate.html", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var template = new StringBuilder(File.ReadAllText(templateFile));
            var stylesFile = string.Format(@"{0}\Resources\ConsortiumExpenses.css", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            var styles = new StringBuilder(File.ReadAllText(stylesFile));


            var finalTickets = new StringBuilder();
            Velocity.Evaluate(velocityContext, new StringWriter(finalTickets), "Payment Tickets", new StringReader(template.ToString()));

            return new ExpensesBillStruct { HtmlExpenses = finalTickets, HtmlExpensesStyles = styles };
        }

        private decimal GetExpensepencentage(decimal total, decimal expenseTotal)
        {
            return (expenseTotal * 100) / total;
        }
    }
}
