using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.List
{
    public class ListRequest
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Customer { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual List<TaskListRequest> Tasks { get; set; }
        public virtual string Coments { get; set; }

    }
}
