﻿using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public IList<Ticket> GetByConsortiumId(int consortiumId)
        {
            var result = this.Context.Set<Ticket>().Where(x => x.Consortium.Id == consortiumId)
                .ToList();
            return result;
        }
    }
}
