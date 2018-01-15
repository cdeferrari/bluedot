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
            this.Property(x => x.Male).IsOptional().HasColumnName("male");
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
            this.HasRequired(x => x.Home).WithMany().Map(x => x.MapKey("home_id"));
            this.HasRequired(x => x.LaborUnion).WithMany().Map(x => x.MapKey("labor_union_id"));
            this.Property(x => x.StartDate).IsRequired().HasColumnName("start_date");
            this.Property(x => x.BirthDate).IsOptional().HasColumnName("birth_date");
            this.Property(x => x.ExtraHourValue).IsOptional().HasColumnName("extra_hour_value");
            this.Property(x => x.FootwearWaist).IsOptional().HasColumnName("footwear_waist");
            this.Property(x => x.ShirtWaist).IsOptional().HasColumnName("shirt_waist");
            this.Property(x => x.PantsWaist).IsOptional().HasColumnName("pants_waist");
            this.Property(x => x.Schedule).IsOptional().HasColumnName("schedule");

        }

    }
}

