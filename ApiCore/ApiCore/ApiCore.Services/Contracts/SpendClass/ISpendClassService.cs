using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Services.Contracts.SpendClasss
{
    public interface ISpendClassService
    {     
        IList<SpendClass> GetAll();     
    }
}
