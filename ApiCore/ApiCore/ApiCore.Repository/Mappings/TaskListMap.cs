using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TaskListMap : EntityMap<TaskList>
    {
        public TaskListMap() : base("tarea_lista")
        {
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            this.Property(x => x.Coments).IsOptional().HasColumnName("coments");
            this.HasRequired(x => x.Status).WithMany().Map(x => x.MapKey("status_id"));
            this.HasRequired(x => x.Result).WithMany().Map(x => x.MapKey("result_id"));            

        }

    }
}

