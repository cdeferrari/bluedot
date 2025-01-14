﻿using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class ProviderRequest
    {        
        public virtual int UserId { get; set; }
        public virtual int Id { get; set; }
        public virtual string Item { get; set; }
        public virtual Address Address { get; set; }
    }
}
