using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class ContactData : Entity
    {
        public virtual int Id { get; set; }
        public virtual string Telephone {get; set;}
        public virtual string Cellphone {get; set;}
        public virtual string Email {get; set;}

    }
}
