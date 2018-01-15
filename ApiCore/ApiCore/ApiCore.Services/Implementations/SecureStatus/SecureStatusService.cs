using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.SecureStatus;
using System.Linq;

namespace ApiCore.Services.Implementations.SecureStatus
{
    public class SecureStatusService : ISecureStatusService
    {
        public ISecureStatusRepository SecureStatusRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.SecureStatus> GetAll()
        {
            return SecureStatusRepository.GetAll().ToList();
            
        }
        

    }
}
