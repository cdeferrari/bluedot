using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class BacklogUser : Entity
    {
        public virtual User User {get; set;}
        public virtual Worker Worker {get; set;}
        public virtual int? OfficeId {get; set;}
        public virtual string Password {get; set;}
        public virtual string Email { get; set; }
        public virtual Role Role {get; set;}        
    }
}
