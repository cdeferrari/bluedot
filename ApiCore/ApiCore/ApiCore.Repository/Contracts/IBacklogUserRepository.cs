﻿using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Contracts
{
    public interface IBacklogUserRepository : IRepository<BacklogUser>
    {
        BacklogUser GetByEmailAndPassword(string email, string password);

    }
}
