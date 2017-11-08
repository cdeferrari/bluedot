using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class BankAccountMap : EntityMap<BankAccount>
    {
        public BankAccountMap() : base("cuenta")
        {            
            this.Property(x => x.CBU).IsRequired().HasColumnName("CBU");
            this.Property(x => x.AccountNumber).IsRequired().HasColumnName("account_number");            
            this.HasRequired(x => x.AccountType).WithMany().Map(x => x.MapKey("account_type_id"));            
        }

    }
}

