﻿using ApiCore.DomainModel;
using System.Collections.Generic;

namespace ApiCore.Dtos.Response
{
    public class OwnershipTicketListResponse
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }        
    }
}
