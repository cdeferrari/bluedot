using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class PaymentTicket
    {
        public string Owner { get; set; }
        public string Department { get; set; }
        public string ExpenseA { get; set; }
        public string ExpenseB { get; set; }
        public string ExpenseC { get; set; }
        public string ExpenseD { get; set; }
        public string ExtraordinaryExpense { get; set; }
        public string Power { get; set; }
        public string Water { get; set; }
        public string Debt { get; set; }
        public string Interest { get; set; }
        public string DiscountTotal { get; set; }
        public string Total { get; set; }
        public int? DiscountDay { get; set; }
    }
}
