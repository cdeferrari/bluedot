using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Account
{
    public class WorkerResponse
    {
        public virtual int Id { get; set; }
        public virtual UserResponse User { get; set; }
    }
}
