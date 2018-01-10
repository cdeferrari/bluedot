using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class CommonDataRequest
    {
        public virtual int Id { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual int CommonDataItemId { get; set; }
        public virtual bool Have { get; set; }

    }
}
