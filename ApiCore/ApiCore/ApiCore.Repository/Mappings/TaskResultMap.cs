using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class TaskResultMap : EntityMap<TaskResult>
    {
        public TaskResultMap() : base("tarea_lista_result")
        {            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");            
        }

    }
}

