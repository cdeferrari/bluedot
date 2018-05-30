using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Control
{
    public class ControlRequest
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual int ProviderId { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime ControlDate { get; set; }
    }
}
