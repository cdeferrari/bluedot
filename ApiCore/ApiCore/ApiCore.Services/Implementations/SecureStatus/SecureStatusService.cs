using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.SecureStatus;

namespace ApiCore.Services.Implementations.SecureStatus
{
    public class SecureStatusService : ISecureStatusService
    {
        public ISecureStatusRepository SecureStatusRepository { get; set; }
        
        
        [Transaction]
        public IList<DomainModel.SecureStatus> GetAll()
        {
            var status = SecureStatusRepository.GetAll();
            if (status == null)
                throw new BadRequestException(ErrorMessages.TicketNoEncontrado);

            var result = new List<DomainModel.SecureStatus>();
            var enumerator = status.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        

    }
}
