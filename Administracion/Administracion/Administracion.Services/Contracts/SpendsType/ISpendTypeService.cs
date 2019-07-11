using Administracion.DomainModel;
using Administracion.Dto.SpendType;
using System.Collections.Generic;

namespace Administracion.Services.Contracts.SpendTypes
{
    public interface ISpendTypeService
    {
        List<SpendType> GetAll();
        List<SpendType> GetByConsortium(int ConsortiumId);
        SpendType GetSpendType(int SpendTypeId);
        Entidad CreateSpendType(SpendTypeRequest SpendType);
        bool UpdateSpendType(SpendTypeRequest SpendType);
        bool DeleteSpendType(int SpendTypeId);        
    }
}
