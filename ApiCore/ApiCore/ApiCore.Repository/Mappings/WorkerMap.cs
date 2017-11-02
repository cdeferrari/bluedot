using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class WorkerMap : EntityMap<Worker>
    {
        public WorkerMap() : base("empleado")
        {
            this.HasRequired(x => x.User).WithMany().Map(x => x.MapKey("user_id"));            
        }

    }
}

