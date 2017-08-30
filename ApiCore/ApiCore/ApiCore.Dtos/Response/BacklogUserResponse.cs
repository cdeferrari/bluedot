using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class BacklogUserResponse
    {
        public virtual UserResponse User { get; set; }
        public virtual WorkerResponse Worker { get; set; }
        public virtual int? OfficeId { get; set; }
        public virtual string Password { get; set; }
        public virtual RoleResponse Role { get; set; }
    }
}
