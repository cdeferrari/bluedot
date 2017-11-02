using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.List
{
    public class TaskListRequest
    {
        public virtual int Id { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int ResultId { get; set; }
        public virtual string Description { get; set; }
        public virtual string Coments { get; set; }

    }
}
