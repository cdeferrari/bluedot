
using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class BacklogUserRepository : Repository<BacklogUser>, IBacklogUserRepository
    {
        public BacklogUser GetByEmailAndPassword(string email, string password)
        {
            var result = this.Context.Set<BacklogUser>().Where(x => x.Email.Equals(email.ToLower()) && x.Password.Equals(password.ToLower()))
                .FirstOrDefault();
            return result;
            
        }
    }
}
