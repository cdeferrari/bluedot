using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using System.Collections.Generic;
using ApiCore.Dtos;
using System.Linq;
using ApiCore.Services.Contracts.SpendTypes;
using ApiCore.Services.Contracts.SpendItems;

namespace ApiCore.Services.Implementations.SpendItems
{

    public class SpendItemService : ISpendItemService
    {
        public ISpendItemRepository SpendItemRepository { get; set; }

        
        [Transaction]
        public IList<SpendItem> GetAll()
        {
            return SpendItemRepository.GetAll().ToList();
            
        }        

    }
}
