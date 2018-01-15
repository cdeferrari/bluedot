using Administracion.DomainModel;
using Administracion.Dto.CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.CommonData
{
    public interface ICommonDataService
    {
        IList<DomainModel.CommonData> GetAll();
        DomainModel.CommonData GetCommonData(int CommonDataId);
        bool CreateCommonData(CommonDataRequest CommonData);
        bool UpdateCommonData(CommonDataRequest CommonData);
        bool DeleteCommonData(int CommonDataId);

        IList<CommonDataItem> GetItems();

    }
}
