using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class CommonDataResponse
    {
        public virtual int Id { get; set; }
        public virtual CommonDataItem Item { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual bool Have { get; set; }
    }
}
