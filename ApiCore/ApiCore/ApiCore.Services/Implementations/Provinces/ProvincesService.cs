using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using ApiCore.Services.Contracts.TicketStatus;
using ApiCore.Services.Contracts.Priorities;
using ApiCore.Services.Contracts.Provinces;
using System.Linq;

namespace ApiCore.Services.Implementations.Provinces
{
    public class ProvincesService : IProvincesService
    {
        public ICountryRepository CountryRepository { get; set; }
        public IProvinceRepository ProvinceRepository { get; set; }
        [Transaction]
        public Province CreateProvince(ProvinceRequest province)
        {
            var entity = new DomainModel.Province()
            {
                Description = province.Description,
                Country = this.CountryRepository.GetById(province.CountryId)
            };
            this.ProvinceRepository.Insert(entity);
            return entity;
            
        }
        [Transaction]
        public void Delete(int provinceId)
        {
            var entity = this.ProvinceRepository.GetById(provinceId);
            this.ProvinceRepository.Delete(entity);
        }

        [Transaction]
        public IList<Province> GetAll()
        {
            return ProvinceRepository.GetAll().ToList();
            
        }


        [Transaction]        
        public IList<Province> GetByCountryId(int id)
        {
            return ProvinceRepository.GetByCountryId(id).ToList();            
        }
    }
}
