using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class UnitAccountStatusSummary
    {
        public virtual string Uf { get; set; }
        public virtual string Piso { get; set; }
        public virtual string Dto { get; set; }
        public virtual string Propietario { get; set; }
        public virtual decimal SaldoAnterior { get; set; }
        public virtual decimal Pagos { get; set; }
        public virtual decimal MesActual { get; set; }
        public virtual decimal Intereses { get; set; }
        public virtual decimal GastosA { get; set; }
        public virtual decimal GastosB { get; set; }
        public virtual decimal GastosC { get; set; }
        public virtual decimal GastosD { get; set; }
        public virtual decimal Expensas { get; set; }
        public virtual decimal Aysa { get; set; }
        public virtual decimal Edesur { get; set; }
        public virtual decimal SiPagaAntes { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int? DiscountDay { get; set; }
        public virtual decimal? DiscountValue { get; set; }
        public virtual decimal GastosAPercent { get; set; }
        public virtual decimal GastosBPercent { get; set; }
    }
}
