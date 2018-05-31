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
            this.Property(x => x.OpenDate).IsRequired().HasColumnName("open_date");
            this.Property(x => x.CloseDate).IsOptional().HasColumnName("close_date");
            this.Property(x => x.LimitDate).IsRequired().HasColumnName("limit_date");
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.Property(x => x.Resolved).IsOptional().HasColumnName("resolved");
            this.Property(x => x.Title).IsRequired().HasColumnName("title");            
            this.HasRequired(x => x.Priority).WithMany().Map(x => x.MapKey("priority_id"));
            this.HasOptional(x => x.Consortium).WithMany().Map(x => x.MapKey("consortium_id"));
            this.HasRequired(x => x.Status).WithMany().Map(x => x.MapKey("status_id"));
            this.HasOptional(x => x.FunctionalUnit).WithMany().Map(x => x.MapKey("functional_unit_id"));
            this.HasOptional(x => x.Worker).WithMany().Map(x => x.MapKey("worker_id"));
            this.HasOptional(x => x.Manager).WithMany().Map(x => x.MapKey("manager_id"));
            this.HasOptional(x => x.BacklogUser).WithMany().Map(x => x.MapKey("backlog_user_id"));
            this.HasOptional(x => x.Area).WithMany().Map(x => x.MapKey("area_id"));
            this.HasRequired(x => x.Creator).WithMany().Map(x => x.MapKey("creator_id"));
            this.HasMany(x => x.Messages).WithRequired(x => x.Ticket).Map(x => x.MapKey("ticket_id"));
            this.HasMany(x => x.Tasks).WithRequired(x => x.Ticket).Map(x => x.MapKey("ticket_id"));
            this.HasMany(x => x.TicketHistory).WithRequired(x => x.Ticket).Map(x => x.MapKey("ticket_id"));
        }

    }
}

