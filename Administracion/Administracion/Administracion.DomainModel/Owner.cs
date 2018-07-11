using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Owner
    {
        public virtual int Id { get; set; }
        public virtual List<int> FunctionalUnitId { get; set; }
        public virtual User User { get; set; }
        public virtual int PaymentTypeId { get; set; }
    }
}
