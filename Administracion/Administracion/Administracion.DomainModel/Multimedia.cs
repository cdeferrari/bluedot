using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Multimedia
    {
        public virtual int Id {get; set;}
        public virtual string Url { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual int MultimediaTypeId { get; set; }


    }
}
