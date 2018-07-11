using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Owner
{
    public class OwnerRequest
    {        
        public virtual int UserId { get; set; }
        public virtual int Id { get; set; }
        public virtual int PaymentTypeId { get; set; }
        public virtual List<int> FunctionalUnitIds { get; set; }
    }
}
