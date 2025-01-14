﻿using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class ConsortiumSecureResponse
    {
        public virtual int Id { get; set; }
        public virtual string PolizyNumber { get; set; }
        public virtual string Brand { get; set; }
        public virtual SecureStatus Status { get; set; }
        public virtual DateTime LimitDate { get; set; }
    }
}
