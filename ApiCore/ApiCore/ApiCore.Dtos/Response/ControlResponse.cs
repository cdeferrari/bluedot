using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class ControlResponse
    {
        public virtual ProviderResponse Provider { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime ControlDate { get; set; }
    }
}
