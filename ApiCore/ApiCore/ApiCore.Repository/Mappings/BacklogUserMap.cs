using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Mappings
{
    public class BacklogUserMap : EntityMap<BacklogUser>
    {
        public BacklogUserMap() : base("usuario_backlog")
        {            
         
            this.Property(x => x.OfficeId).IsOptional().HasColumnName("office_id");            
            this.Property(x => x.Password).IsRequired().HasColumnName("password");
            this.Property(x => x.Email).IsRequired().HasColumnName("email");
            this.HasRequired(x => x.Role).WithMany().Map(x => x.MapKey("role_id"));

            this.HasOptional(x => x.Worker).WithMany().Map(x => x.MapKey("worker_id"));
            this.HasOptional(x => x.User).WithMany().Map(x => x.MapKey("user_id"));
        }

    }
}

