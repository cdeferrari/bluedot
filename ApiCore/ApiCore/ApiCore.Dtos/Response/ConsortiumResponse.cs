using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class ConsortiumResponse
    {
        public virtual int Id { get; set; }
        public virtual string FriendlyName { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string MailingList { get; set; }
        public virtual AdministrationResponse Administration { get; set; }        
        
        public virtual OwnershipResponse Ownership { get; set; }

        public virtual IList<ManagerResponse> Managers { get; set; }
    }
}
