using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class ConsortiumBalanceItem
    {
        public virtual string Uf { get; set; }
        public virtual string Piso { get; set; }
        public virtual string Dto { get; set; }
        public virtual string Propietario { get; set; }
        public virtual string SaldoAnterior { get; set; }
        public virtual string Pagos { get; set; }
        public virtual string Deuda { get; set; }
        public virtual string Intereses { get; set; }
        public virtual string GastosA { get; set; }
        public virtual string GastosB { get; set; }
        public virtual string Expensas { get; set; }
        public virtual string Aysa { get; set; }
        public virtual string Edesur { get; set; }
        public virtual string SiPagaAntes { get; set; }
        public virtual string Total { get; set; }
        public virtual string DiscountValue { get; set; }
        public virtual string GastosAPercent { get; set; }
        public virtual string GastosBPercent { get; set; }
    }
}
