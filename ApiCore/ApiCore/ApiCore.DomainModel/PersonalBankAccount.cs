using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.DomainModel
{
    public class PersonalBankAccount : Entity
    {
        public virtual BankAccount BankAccount {get; set;}        
    }   
}
