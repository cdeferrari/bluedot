using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class IncomeTypeRequest
    {        
        public virtual bool Required { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }        
        
    }
}
