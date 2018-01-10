using ApiCore.DomainModel;
using System.Collections.Generic;


namespace ApiCore.Services.Contracts.CommonDataItems
{
    public interface ICommonDataItemsService
    {
        
        IList<CommonDataItem> GetAll();
        
    }
}
