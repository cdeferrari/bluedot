using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Unit
{
    public interface IUnitService
    {
        FunctionalUnit GetById(int unitId);
    }
}
