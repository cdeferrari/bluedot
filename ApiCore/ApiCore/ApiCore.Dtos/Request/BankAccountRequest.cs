﻿using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class BankAccountRequest
    {
        public virtual string DNI { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual ContactData ContactData { get; set; }
        
    }
}
