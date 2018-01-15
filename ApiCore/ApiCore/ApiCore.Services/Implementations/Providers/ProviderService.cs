using ApiCore.Services.Contracts.Providers;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Providers
{
    public class ProviderService : IProviderService
    { 
        public IProviderRepository ProviderRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        

        [Transaction]
        public Provider CreateProvider(ProviderRequest Provider)
        {

            var entityToInsert = new Provider()
            {
                User = this.UserRepository.GetById(Provider.UserId),
                Item = Provider.Item,
                Address = Provider.Address

            };
            ProviderRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Provider GetById(int ProviderId)
        {
            var Provider = ProviderRepository.GetById(ProviderId);
            if (Provider == null)
                throw new BadRequestException(ErrorMessages.TrabajadorNoEncontrado);
            
            return Provider;

        }
        

        [Transaction]
        public Provider UpdateProvider(Provider originalProvider, ProviderRequest Provider)
        {            
            this.MergeProvider(originalProvider, Provider);
            ProviderRepository.Update(originalProvider);
            return originalProvider;

        }
        

        [Transaction]
        public void DeleteProvider(int ProviderId)
        {
            var Provider = ProviderRepository.GetById(ProviderId);
            ProviderRepository.Delete(Provider);
        }
        

        private void MergeProvider(Provider originalProvider, ProviderRequest Provider)
        {            
            originalProvider.User = this.UserRepository.GetById(Provider.UserId);
            originalProvider.Item = Provider.Item;
        }

        [Transaction]
        public List<Provider> GetAll()
        {
            return ProviderRepository.GetAll().ToList();
            
        }

    }
}
