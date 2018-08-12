using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.UnitConfigurations
{
    public class UnitConfigurationRequest
    {        
        public virtual int UnitConfigurationTypeId { get; set; }
        public virtual int UnitId { get; set; }
        public virtual decimal Value { get; set; }
     
    }
}
