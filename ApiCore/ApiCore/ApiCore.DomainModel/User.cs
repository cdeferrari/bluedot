using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class User : Entity
    {
         public virtual string DNI { get; set; }
         public virtual string CUIT { get; set; }
         public virtual string Name { get; set; }
         public virtual string Surname { get; set; }
         public virtual string ProfilePic { get; set; }
         public virtual ContactData ContactData {get; set;}
         public virtual List<MultimediaUsuario> Multimedia { get; set; }

    }
}
