﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class RenterRequest
    {        
        public virtual int UserId { get; set; }
        public virtual int Id { get; set; }
        public virtual int PaymentTypeId { get; set; }

        public virtual int FunctionalUnitId { get; set; }
    }
}
