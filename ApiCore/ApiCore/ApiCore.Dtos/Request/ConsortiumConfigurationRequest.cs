using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class ConsortiumConfigurationRequest
    {        
        public virtual int ConsortiumConfigurationTypeId { get; set; }
        public virtual int ConsortiumId { get; set; }        
        public virtual decimal Value { get; set; }        
        
    }
}
