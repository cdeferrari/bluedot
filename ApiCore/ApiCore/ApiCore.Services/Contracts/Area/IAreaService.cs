using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.Area
{
    public interface IAreaService
    {     
        IList<DomainModel.Area> GetAll();     
    }
}
