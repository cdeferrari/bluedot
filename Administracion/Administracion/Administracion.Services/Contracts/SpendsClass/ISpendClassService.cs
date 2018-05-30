using Administracion.DomainModel;
using Administracion.Dto.SpendType;
using System.Collections.Generic;

namespace Administracion.Services.Contracts.SpendClass
{
    public interface ISpendClassService
    {
        List<DomainModel.SpendClass> GetAll();
    }
}
