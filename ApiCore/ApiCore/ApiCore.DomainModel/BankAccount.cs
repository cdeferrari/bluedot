using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class BankAccount : Entity
    {
         public virtual AccountType AccountType { get; set; }
         public virtual string CBU { get; set; }
         public virtual string AccountNumber { get; set; }

    }
}

