using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.ConsortiumConfigurations
{
    public class ConsortiumConfigurationRequest
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumConfigurationTypeId { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Value { get; set; }
        public virtual DateTime ConfigurationDate { get; set; }

    }
}
