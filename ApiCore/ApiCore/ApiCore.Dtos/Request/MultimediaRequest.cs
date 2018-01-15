using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class MultimediaRequest
    {
        public virtual string Url { get; set; }
        public virtual int MultimediaTypeId { get; set; }
        public virtual int OwnershipId { get; set; }
    }
}
