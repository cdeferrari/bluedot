﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Account
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
