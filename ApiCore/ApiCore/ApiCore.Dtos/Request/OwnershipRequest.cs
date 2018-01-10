using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class OwnershipRequest
    {        
        public virtual string Category { get; set; }
        public virtual Address Address { get; set; }        
    }
}
