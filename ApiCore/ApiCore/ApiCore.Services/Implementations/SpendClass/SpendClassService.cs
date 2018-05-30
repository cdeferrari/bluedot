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
using ApiCore.Services.Contracts.SpendClasss;

namespace ApiCore.Services.Implementations.SpendClass
{

    public class SpendClassService : ISpendClassService
    {
        public ISpendClassRepository SpendClassRepository { get; set; }
        
        [Transaction]
        public IList<DomainModel.SpendClass> GetAll()
        {
            return SpendClassRepository.GetAll().ToList();
            
        }
    }
}
