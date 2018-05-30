using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TaskHistoryMap : EntityMap<TaskHistory>
    {
        public TaskHistoryMap() : base("seguimiento_tarea")
        {
            
            this.Property(x => x.Coment).IsRequired().HasColumnName("coment");
            this.Property(x => x.FollowDate).IsRequired().HasColumnName("follow_date");
            this.Property(x => x.Coment).IsRequired().HasColumnName("coment");
            this.HasRequired(x => x.Task).WithMany().Map(x => x.MapKey("task_id"));

        }

    }
}

