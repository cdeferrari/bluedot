using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCore.DomainModel;

namespace ApiCore.Repository.Mappings
{
    public class BillMap : EntityMap<Bill>
    {
        public BillMap() : base("Factura")
        {
            this.HasOptional(x => x.Provider).WithMany().Map(x => x.MapKey("provider_id"));
            this.HasOptional(x => x.Worker).WithMany().Map(x => x.MapKey("worker_id"));
            this.HasOptional(x => x.Manager).WithMany().Map(x => x.MapKey("manager_id"));


            this.Property(x => x.Number).IsRequired().HasColumnName("bill_number");
            this.Property(x => x.ClientNumber).IsOptional().HasColumnName("client_number");
            this.Property(x => x.Amount).IsRequired().HasColumnName("amount");

            this.Property(x => x.CreationDate).IsRequired().HasColumnName("creation_date");
            this.Property(x => x.ExpirationDate).IsOptional().HasColumnName("expiration_date");
            this.Property(x => x.NextExpirationDate).IsRequired().HasColumnName("next_expiration_date");
         
        }

    }
}

