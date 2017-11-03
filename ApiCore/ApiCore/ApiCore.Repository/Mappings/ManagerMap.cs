using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class ManagerMap : EntityMap<Manager>
    {
        public ManagerMap() : base("encargado")
        {
                        
            this.Property(x => x.Salary).IsRequired().HasColumnName("salary");
            this.Property(x => x.WorkInsurance).IsRequired().HasColumnName("work_insurance");
            this.Property(x => x.IsAlternate).IsRequired().HasColumnName("is_alternate");
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
            this.HasRequired(x => x.Home).WithMany().Map(x => x.MapKey("home_id"));
            this.HasRequired(x => x.LaborUnion).WithMany().Map(x => x.MapKey("labor_union_id"));
            this.HasRequired(x => x.JobDomicile).WithMany().Map(x => x.MapKey("job_domicile_id"));
            this.Property(x => x.StartDate).IsRequired().HasColumnName("start_date");

        }

    }
}

