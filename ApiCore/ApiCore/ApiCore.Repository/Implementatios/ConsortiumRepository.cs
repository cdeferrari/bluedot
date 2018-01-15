using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class ConsortiumRepository : Repository<Consortium>, IConsortiumRepository
    {
        public IQueryable<Consortium> GetAllActives()
        {
            return Entities.Where(x=> !x.Inactive).AsQueryable();
        }
        public void LogicDelete(Consortium consortium)
        {
            consortium.Inactive = true;
            this.Update(consortium);
        }
    }
}
