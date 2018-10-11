using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class UnitAccountStatusSummary
    {
        public virtual string Piso { get; set; }
        public virtual string Dto { get; set; }
        public virtual decimal Pagos { get; set; }
        public virtual decimal Deuda { get; set; }
        public virtual decimal Intereses { get; set; }
        public virtual decimal GastosA { get; set; }
        public virtual decimal GastosB { get; set; }
        public virtual decimal Aysa { get; set; }
        public virtual decimal Edesur { get; set; }
        public virtual decimal SiPagaAntes { get; set; }
        public virtual decimal Total { get; set; }
        
    }
}
