using ApiCore.Services.Contracts.Users;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Repository.Contracts;
using System.Collections.Generic;
using ApiCore.Services.Contracts.Unit;

namespace ApiCore.Services.Implementations.Users
{
    public class UnitService : IUnitService
    {
        public IFunctionalUnitRepository UnitRepository { get; set; }
       
        FunctionalUnit IUnitService.GetById(int unitId)
        {
            return this.UnitRepository.GetById(unitId);
        }

        List<FunctionalUnit> IUnitService.GetAll()
        {
            var units = UnitRepository.GetAll();
            if (units == null)
                throw new BadRequestException(ErrorMessages.UnidadNoEncontrada);

            var result = new List<FunctionalUnit>();
            var enumerator = units.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }
    }
}
