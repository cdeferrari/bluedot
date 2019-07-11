using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class UnitPaymentViewModel
    {
        public int Id { get; set; }
        public int ConsortiumId { get; set; }
        public int UnitId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual IEnumerable<SelectListItem> PaymentTypes { get; set; }
        public virtual int PaymentTypeId { get; set; }
        public virtual FunctionalUnitViewModel FunctionalUnit { get; set; }
    }
}