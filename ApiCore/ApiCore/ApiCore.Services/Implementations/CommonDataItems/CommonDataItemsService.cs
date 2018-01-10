using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.CommonDataItems;

namespace ApiCore.Services.Implementations.CommonDataItems
{
    public class CommonDataItemsService : ICommonDataItemsService
    {
        public ICommonDataItemsRepository CommonDataItemsRepository { get; set; }
        
        
        [Transaction]
        public IList<CommonDataItem> GetAll()
        {
            var status = CommonDataItemsRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<CommonDataItem>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
