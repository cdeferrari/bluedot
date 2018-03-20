using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Bill
    {
        public virtual int Id { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string Number { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual DateTime NextExpirationDate { get; set; }
    }
}
