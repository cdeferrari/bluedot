﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Worker
{
    public class WorkerRequest
    {        
        public virtual int AdministrationId { get; set; }    
        public virtual int UserId { get; set; }
        public virtual int Id { get; set; }
    }
}
