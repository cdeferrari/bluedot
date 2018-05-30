using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class TicketHistory
    {
        public virtual int Id { get; set; }
        public virtual string Coment { get; set; }
        public virtual DateTime FollowDate { get; set; }
    }
}
