using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Renter
    {
        public virtual int Id { get; set; }
        public virtual int FunctionalUnitId { get; set; }
        public virtual User User { get; set; }
    }
}
