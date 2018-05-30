using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using ApiCore.Dtos;
using System.Linq;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Area;

namespace ApiCore.Services.Implementations.Area
{

    public class AreaService : IAreaService
    {
        public IAreaRepository AreaRepository { get; set; }
        
        [Transaction]
        public IList<DomainModel.Area> GetAll()
        {
            return AreaRepository.GetAll().ToList();
            
        }
    }
}
