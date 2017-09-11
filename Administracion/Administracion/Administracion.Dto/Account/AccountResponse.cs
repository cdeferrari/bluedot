using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Account
{
    public class AccountResponse
    {
        public virtual int Id { get; set; }
        public virtual UserResponse User { get; set; }
       public virtual WorkerResponse Worker { get; set; }       
       public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual RoleResponse Role { get; set; }
       
    }
}
