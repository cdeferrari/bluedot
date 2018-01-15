using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Province
{
    public class ProvinceRequest
    {
        public virtual int CountryId { get; set; }
        public virtual string Description { get; set; }
    }
}
