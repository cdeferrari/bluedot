﻿using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Income
{
    public class IncomeRequest
    {
        public virtual int Id { get; set; }
        public virtual int IncomeTypeId { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime IncomeDate { get; set; }

    }
}
