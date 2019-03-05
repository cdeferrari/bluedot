using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Consortium
{
    public class ConsortiumRequest
    {
        public virtual int Id { get; set; }
        public virtual string FriendlyName { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string MailingList { get; set; }
        public virtual string Telephone { get; set; }
        public virtual int AdministrationId { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual string ClaveSuterh { get; set; }
        public virtual string Juicios { get; set; }
    }
}
