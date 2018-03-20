using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Bill
{
    public class BillRequest
    {
        public virtual int Id { get; set; }
        public virtual int? ProviderId { get; set; }
        public virtual int? WorkerId { get; set; }
        public virtual int? ManagerId { get; set; }
        public virtual string Number { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime NextExpirationDate { get; set; }

    }
}
