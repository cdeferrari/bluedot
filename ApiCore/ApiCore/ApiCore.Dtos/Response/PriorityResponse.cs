using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class PriorityResponse
    {
        public virtual int Id { get; set; }
        public virtual int Value { get; set; }
        public virtual string Description { get; set; }
    }
}
