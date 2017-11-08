using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class PesonalAccountMap : EntityMap<PersonalBankAccount>
    {
        public PesonalAccountMap() : base("cuenta_usuario")
        {
            this.HasRequired(x => x.BankAccount).WithMany().Map(x => x.MapKey("user_id"));
        }

    }
}

