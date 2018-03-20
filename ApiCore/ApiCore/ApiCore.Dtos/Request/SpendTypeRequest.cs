using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class SpendTypeRequest
    {
        public virtual int SpendItemId { get; set; }
        public virtual bool Required { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }        
        public virtual bool ForAll { get; set; }
    }
}
