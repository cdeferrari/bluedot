using Administracion.DomainModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public virtual Roles Role { get; set; }

        public virtual string Email { get; set; }

        public virtual User User { get; set; }
       
    }
}
