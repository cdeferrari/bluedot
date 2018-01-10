using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class MultimediaUsuario : Entity
    {
         public virtual string Url { get; set; }    
        public virtual User User { get; set; }

    }
}
