using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class PatrimonyStatus
    {
        public virtual int Id { get; set; }
        public virtual DateTime StatusDate { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual decimal Debe { get; set; }
        public virtual decimal Haber { get; set; }
        public virtual decimal Activo { get; set; }
        public virtual decimal Pasivo { get; set; }
    }
}
