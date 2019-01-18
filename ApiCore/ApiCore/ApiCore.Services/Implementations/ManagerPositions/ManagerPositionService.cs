using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.TaskResult;
using ApiCore.Services.Contracts.ManagerPositions;
using ApiCore.Dtos;
using System.Linq;
using ApiCore.Services.Contracts.ManagerPositions;

namespace ApiCore.Services.Implementations.ManagerPositions
{

    public class ManagerPositionService : IManagerPositionService
    {
        public IManagerPositionRepository ManagerPositionRepository { get; set; }


        [Transaction]
        public ManagerPosition CreateManagerPosition(DescriptionRequest ManagerPosition)
        {
            var entity = new ManagerPosition() { Description = ManagerPosition.Description };
            this.ManagerPositionRepository.Insert(entity);
            return entity;
        }
        [Transaction]
        public void Delete(int ManagerPositionId)
        {
            var entity = this.ManagerPositionRepository.GetById(ManagerPositionId);
            this.ManagerPositionRepository.Delete(entity);
        }

        [Transaction]
        public IList<ManagerPosition> GetAll()
        {
            return ManagerPositionRepository.GetAll().ToList();            
        }
        

    }
}
