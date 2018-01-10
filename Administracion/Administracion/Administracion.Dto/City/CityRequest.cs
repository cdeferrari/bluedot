using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.City
{
    public class CityRequest
    {
        public virtual int ProvinceId { get; set; }
        public virtual string Description { get; set; }
    }
}
