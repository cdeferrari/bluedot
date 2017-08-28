using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TicketMap : EntityMap<Ticket>
    {
        public TicketMap() : base("Ticket")
        {
            
            this.Property(x => x.Customer).IsRequired().HasColumnName("customer");
            this.Property(x => x.StatusId).IsRequired().HasColumnName("status_id");
            this.Property(x => x.OpenDate).IsRequired().HasColumnName("open_date");
            this.Property(x => x.CloseDate).IsOptional().HasColumnName("close_date");
            this.Property(x => x.LimitDate).IsRequired().HasColumnName("limit_date");
            this.Property(x => x.ConsortiumId).IsRequired().HasColumnName("consortium_id");
            this.Property(x => x.AdministrationId).IsRequired().HasColumnName("administration_id");
            this.Property(x => x.FunctionalUnitId).IsRequired().HasColumnName("functional_unit_id");
            
            this.HasRequired(x => x.Priority).WithMany().Map(x => x.MapKey("priority_id"));
            this.Property(x => x.WorkerId).IsRequired().HasColumnName("worker_id");
            this.Property(x => x.CreatorId).IsRequired().HasColumnName("creator_id");


        }

    }
}

