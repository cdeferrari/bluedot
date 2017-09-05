using ApiCore.Services.Implementations.BacklogUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Implementations.BacklogUser
{
    public class BacklogUserService : IBacklogUserService
    {
        public virtual IBacklogUserRepository BacklogUserRepository { get; set; }

        public DomainModel.BacklogUser GetByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
