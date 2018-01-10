using Administracion.DomainModel;
using Administracion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.LaboralUnion
{
    public interface ILaboralUnionService
    {
        IList<DomainModel.LaboralUnion> GetAll();
        bool CreateLaboralUnion(DescriptionRequest laboralUnion);

        bool DeleteLaboralUnion(int id);
    }
}
