using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Spend
{
    public class SpendRequest
    {
        public virtual int Id { get; set; }
        public virtual int BillId { get; set; }
        public virtual int SpendTypeId { get; set; }
        public virtual int SpendClassId { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime PaymentDate { get; set; }

        public virtual int? TaskId { get; set; }
    }
}
