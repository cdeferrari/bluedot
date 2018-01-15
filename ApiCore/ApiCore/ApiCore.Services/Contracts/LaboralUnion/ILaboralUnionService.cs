using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.LaboralUnion
{
    public interface ILaboralUnionService
    {        
        IList<DomainModel.LaboralUnion> GetAll();
        DomainModel.LaboralUnion CreateLaboralUnion(DescriptionRequest LaboralUnion);

        void Delete(int laboralUnionId);
    }
}
