﻿
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class AddressMap : EntityMap<Address>
    {
        public AddressMap() : base("direccion")
        {
            
            this.Property(x => x.Street).IsRequired().HasColumnName("street");
            this.Property(x => x.Number).IsRequired().HasColumnName("number");
            
            this.Property(x => x.Lat).IsOptional().HasColumnName("latitud");
            this.Property(x => x.Len).IsOptional().HasColumnName("longitud");
            this.Property(x => x.PostalCode).IsOptional().HasColumnName("postal_code");
            this.Property(x => x.City).IsOptional().HasColumnName("city");
            this.Property(x => x.Province).IsOptional().HasColumnName("province");

        }

    }
}

