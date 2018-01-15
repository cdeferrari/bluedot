using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
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
            return this.BacklogUserRepository.GetByEmailAndPassword(email, password);
        }

        public List<DomainModel.BacklogUser> GetAll()
        {
            return this.BacklogUserRepository.GetAll().ToList();            
        }

    }

}
