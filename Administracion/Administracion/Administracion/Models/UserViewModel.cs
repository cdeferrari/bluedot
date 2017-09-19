using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class UserViewModel
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string DNI { get; set; }
        public virtual string CUIT { get; set; }
        public virtual DataContactViewModel DataContact { get; set; }        
    }
}