﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.DomainModel
{
    public class Spend
    {
        public virtual int Id { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual Consortium Consortium { get; set; }
        public virtual SpendType Type { get; set; }
        public virtual SpendClass SpendClass { get; set; }
        public virtual DateTime PaymentDate { get; set; }
        public virtual string Description { get; set; }
    }
}
