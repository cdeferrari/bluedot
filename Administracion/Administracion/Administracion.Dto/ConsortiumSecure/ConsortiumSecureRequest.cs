using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.ConsortiumSecure
{
    public class ConsortiumSecureRequest
    {
        public virtual int Id { get; set; }
        public virtual int SecureStatusId { get; set; }
        public virtual string PolizyNumber { get; set; }
        public virtual string Brand { get; set; }
        public virtual DateTime LimitDate { get; set; }
        public virtual int ConsortiumId { get; set; }
    }
}
