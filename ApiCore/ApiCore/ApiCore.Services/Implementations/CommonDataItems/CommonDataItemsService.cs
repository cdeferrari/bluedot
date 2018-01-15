using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.CommonDataItems;
using System.Linq;

namespace ApiCore.Services.Implementations.CommonDataItems
{
    public class CommonDataItemsService : ICommonDataItemsService
    {
        public ICommonDataItemsRepository CommonDataItemsRepository { get; set; }
        
        
        [Transaction]
        public IList<CommonDataItem> GetAll()
        {
            return CommonDataItemsRepository.GetAll().ToList();
        }
        

    }
}
