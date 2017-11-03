using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class Address : Entity
    {
        public virtual string Street { get; set; }
        public virtual int Number { get; set; }
        public virtual double? Lat { get; set; }
        public virtual double? Len { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }

    }
}
