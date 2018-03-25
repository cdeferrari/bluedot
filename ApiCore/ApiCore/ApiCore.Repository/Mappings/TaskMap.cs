
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TaskMap : EntityMap<Task>
    {
        public TaskMap() : base("Tarea")
        {
            this.Property(x => x.OpenDate).IsRequired().HasColumnName("open_date");
            this.Property(x => x.CloseDate).IsOptional().HasColumnName("close_date");            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");               
            this.HasRequired(x => x.Priority).WithMany().Map(x => x.MapKey("priority_id"));
            this.HasOptional(x => x.Ticket).WithMany().Map(x => x.MapKey("ticket_id"));
            this.HasRequired(x => x.Status).WithMany().Map(x => x.MapKey("status_id"));            
            this.HasOptional(x => x.Worker).WithMany().Map(x => x.MapKey("worker_id"));
            this.HasOptional(x => x.Provider).WithMany().Map(x => x.MapKey("provider_id"));
            this.HasOptional(x => x.Manager).WithMany().Map(x => x.MapKey("manager_id"));
            this.HasRequired(x => x.Creator).WithMany().Map(x => x.MapKey("creator_id"));
            this.HasMany(x => x.Spends).WithOptional(x => x.Task).Map(x => x.MapKey("task_id"));
        }

    }
}

