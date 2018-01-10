using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class CommonData
    {
        public virtual CommonDataItem Item { get; set; }
        public virtual bool Have { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual int Id { get; set; }
    }
}
