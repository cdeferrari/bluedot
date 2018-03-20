using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class IncomeTypeMap : EntityMap<IncomeType>
    {
        public IncomeTypeMap() : base("tipo_ingreso")
        {            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.Property(x => x.Required).IsRequired().HasColumnName("required");            
            this.HasRequired(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));

        }

    }
}

