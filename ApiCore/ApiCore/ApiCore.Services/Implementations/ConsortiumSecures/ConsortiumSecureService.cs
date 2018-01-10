using ApiCore.Services.Contracts.ConsortiumSecures;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using ApiCore.Services.Contracts.ConsortiumSecures;

namespace ApiCore.Services.Implementations.ConsortiumSecures
{
    public class ConsortiumSecureService : IConsortiumSecureService
    {
        public virtual IConsortiumSecureRepository ConsortiumSecureRepository { get; set; }
        public virtual ISecureStatusRepository SecureStatusRepository { get; set; }
        public virtual IConsortiumRepository ConsortiumRepository { get; set; }


        [Transaction]
        public ConsortiumSecure CreateConsortiumSecure(ConsortiumSecureRequest ConsortiumSecure)
        {
            var entityToInsert = new ConsortiumSecure()
            {
                Brand =ConsortiumSecure.Brand,
                Consortium = this.ConsortiumRepository.GetById(ConsortiumSecure.ConsortiumId),
                LimitDate = ConsortiumSecure.LimitDate,
                PolizyNumber = ConsortiumSecure.PolizyNumber,
                Status = this.SecureStatusRepository.GetById(ConsortiumSecure.SecureStatusId)

            };

            ConsortiumSecureRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public ConsortiumSecure GetById(int ConsortiumSecureId)
        {
            var ConsortiumSecure = ConsortiumSecureRepository.GetById(ConsortiumSecureId);
            if (ConsortiumSecure == null)
                throw new BadRequestException(ErrorMessages.ConsorcioNoEncontrado);

            return ConsortiumSecure;
        }


        [Transaction]
        public ConsortiumSecure UpdateConsortiumSecure(ConsortiumSecure originalConsortiumSecure, ConsortiumSecureRequest ConsortiumSecure)
        {
            this.MergeConsortiumSecure(originalConsortiumSecure, ConsortiumSecure);
            ConsortiumSecureRepository.Update(originalConsortiumSecure);
            return originalConsortiumSecure;
        }


        [Transaction]
        public void DeleteConsortiumSecure(int ConsortiumSecureId)
        {
            var ConsortiumSecure = ConsortiumSecureRepository.GetById(ConsortiumSecureId);
            ConsortiumSecureRepository.Delete(ConsortiumSecure);
        }


        private void MergeConsortiumSecure(ConsortiumSecure originalConsortiumSecure, ConsortiumSecureRequest ConsortiumSecure)
        {
            originalConsortiumSecure.Brand = ConsortiumSecure.Brand;
            originalConsortiumSecure.Consortium = this.ConsortiumRepository.GetById(ConsortiumSecure.ConsortiumId);
            originalConsortiumSecure.LimitDate = ConsortiumSecure.LimitDate;
            originalConsortiumSecure.PolizyNumber = ConsortiumSecure.PolizyNumber;
            originalConsortiumSecure.Status = this.SecureStatusRepository.GetById(ConsortiumSecure.SecureStatusId);   
        }

        [Transaction]
        public List<ConsortiumSecure> GetAll()
        {
            var secure = ConsortiumSecureRepository.GetAll();
            if (secure == null)
                throw new BadRequestException(ErrorMessages.UserNoEncontrado);

            var result = new List<ConsortiumSecure>();
            var enumerator = secure.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }

    }
}

