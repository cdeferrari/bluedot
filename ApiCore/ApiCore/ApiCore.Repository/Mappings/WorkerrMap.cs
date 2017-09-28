using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class WorkerrMap : EntityMap<Worker>
    {
        public WorkerrMap() : base("empleado")
        {
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));            
            this.Property(x => x.AdministrationId).IsRequired().HasColumnName("administration_id");
            
            
        }

    }
}

