﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class SpendItemMap : EntityMap<SpendItem>
    {
        public SpendItemMap() : base("rubro_gasto")
        {
            
            this.Property(x => x.Description).IsRequired().HasColumnName("description");
            
        }

    }
}

