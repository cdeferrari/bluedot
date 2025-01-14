﻿using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class AccountStatusRequest
    {          
        public virtual int Id { get; set; }      
        public virtual int UnitId { get; set; }        
        public virtual decimal Debe { get; set; }
        public virtual decimal Haber { get; set; }
        public virtual DateTime StatusDate { get; set; }
        public virtual int? PaymentTypeId { get; set; }
    }
}
