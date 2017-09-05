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

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual string Email { get; set; }
    }
}
