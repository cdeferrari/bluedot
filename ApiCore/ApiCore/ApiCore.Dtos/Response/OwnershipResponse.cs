﻿using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Dtos.Response
{
    public class OwnershipResponse
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }
        public virtual string Category { get; set; }
        public virtual List<FunctionalUnitResponse> FunctionalUnits {get;set;}
        public virtual List<CommonDataResponse> CommonData { get; set; }
        public virtual List<Multimedia> Multimedia { get; set; }
    }
}
