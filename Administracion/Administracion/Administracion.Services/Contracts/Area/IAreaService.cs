using Administracion.DomainModel;
using Administracion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.AreaService
{
    public interface IAreaService
    {
        IList<Area> GetAll();        
    }
}
