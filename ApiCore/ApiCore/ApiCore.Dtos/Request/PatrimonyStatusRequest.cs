using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class PatrimonyStatusRequest
    {                
        public virtual int ConsortiumId { get; set; }        
        public virtual decimal Debe { get; set; }
        public virtual decimal Haber { get; set; }
        public virtual decimal Activo { get; set; }
        public virtual decimal Pasivo { get; set; }
        public virtual DateTime StatusDate { get; set; }
        
    }
}
