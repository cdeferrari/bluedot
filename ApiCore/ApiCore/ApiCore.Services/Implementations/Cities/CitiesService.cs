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
using ApiCore.Services.Contracts.Cities;

namespace ApiCore.Services.Implementations.Citys
{
    public class CitiesService : ICitiesService
    {
        
        public ICityRepository CityRepository { get; set; }
        public IProvinceRepository ProvinceRepository { get; set; }

        public City CreateCity(CityRequest City)
        {
            var entity = new DomainModel.City()
            {
                Description = City.Description,
                Province = this.ProvinceRepository.GetById(City.ProvinceId)
            };
            this.CityRepository.Insert(entity);
            return entity;
            
        }

        public void Delete(int CityId)
        {
            var entity = this.CityRepository.GetById(CityId);
            this.CityRepository.Delete(entity);
        }

        [Transaction]
        public IList<City> GetAll()
        {
            var Citys = CityRepository.GetAll();
            if (Citys == null)
                throw new BadRequestException(ErrorMessages.ProvinciaNoEncontrada);

            var result = new List<DomainModel.City>();
            var enumerator = Citys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;

        }
        
    }
}
