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

namespace ApiCore.Services.Implementations.Provinces
{
    public class ProvincesService : IProvincesService
    {
        public ICountryRepository CountryRepository { get; set; }
        public IProvinceRepository ProvinceRepository { get; set; }

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

        public void Delete(int provinceId)
        {
            var entity = this.ProvinceRepository.GetById(provinceId);
            this.ProvinceRepository.Delete(entity);
        }

        [Transaction]
        public IList<Province> GetAll()
        {
            var provinces = ProvinceRepository.GetAll();
            if (provinces == null)
                throw new BadRequestException(ErrorMessages.ProvinciaNoEncontrada);

            var result = new List<DomainModel.Province>();
            var enumerator = provinces.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;

        }


        [Transaction]        
        public IList<Province> GetByCountryId(int id)
        {
            var provinces = ProvinceRepository.GetByCountryId(id);
            if (provinces == null)
                throw new BadRequestException(ErrorMessages.ProvinciaNoEncontrada);

            var result = new List<DomainModel.Province>();
            var enumerator = provinces.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;            
        }
    }
}
