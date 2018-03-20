using ApiCore.DomainModel;
using ApiCore.Dtos;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.SpendTypes
{
    public interface ISpendTypeService
    {
        SpendType GetById(int SpendId);
        IList<SpendType> GetAll();
        SpendType CreateSpendType(SpendTypeRequest SpendType);
        SpendType UpdateSpendType(SpendType originalSpend, SpendTypeRequest Spend);
        void Delete(int SpendTypeId);
    }
}
