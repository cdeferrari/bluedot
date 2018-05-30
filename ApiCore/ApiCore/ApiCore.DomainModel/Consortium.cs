using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Consortium : Entity
    {
         public virtual string FriendlyName { get; set; }
         public virtual string CUIT { get; set; }
         public virtual string MailingList { get; set; }
        public virtual string Telephone { get; set; }
        public virtual Administration Administration {get; set;}
         public virtual Ownership Ownership {get; set;}
         public virtual IList<Manager> Managers { get; set; }
        public virtual IList<ConsortiumSecure> ConsortiumSecure { get; set; }
        public virtual bool Inactive { get; set; }
        public virtual IList<FireExtinguisherControl> FireExtinguisherControls { get; set; }
        public virtual IList<ElevatorControl> ElevatorControls { get; set; }

    }
}
