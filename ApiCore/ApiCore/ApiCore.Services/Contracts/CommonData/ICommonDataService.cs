using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.CommonDatas
{
    public interface ICommonDataService
    {
        CommonData CreateCommonData(CommonDataRequest CommonData);
        CommonData GetById(int CommonDataId);
        IList<CommonData> GetAll();
        IList<CommonData> GetByOwnership(int id);
        CommonData UpdateCommonData(CommonData originalCommonData, CommonDataRequest CommonData);
        void DeleteCommonData(int CommonDataId);
    }
}
