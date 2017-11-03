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
using AutoMapper;

namespace ApiCore.Services.Implementations.Users
{
    public class UnitService : IUnitService
    {
        public IFunctionalUnitRepository UnitRepository { get; set; }
        public IOwnershipRepository OwnershipRepository { get; set; }
        public IOwnerRepository OwnerRepository { get; set; }
        public IRenterRepository RenterRepository { get; set; }

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

        [Transaction]
        public FunctionalUnit CreateUnit(FunctionalUnitRequest request)
        {
            var unitToInsert = new FunctionalUnit();

            var entityToInsert = MergeUnit(unitToInsert, request);

            UnitRepository.Insert(entityToInsert);            

            return entityToInsert;
        }

        private FunctionalUnit MergeUnit(FunctionalUnit originalUnit,  FunctionalUnitRequest unit)
        {
            originalUnit.Dto = unit.Dto;
            originalUnit.Floor = unit.Floor;            
            originalUnit.Ownership = this.OwnershipRepository.GetById(unit.OwnershipId);
            
            return originalUnit;
        }

        [Transaction]
        public FunctionalUnit UpdateUnit(FunctionalUnit originalFunctionalUnit, FunctionalUnitRequest unit)
        {
            this.MergeUnit(originalFunctionalUnit, unit);
            UnitRepository.Update(originalFunctionalUnit);
            return originalFunctionalUnit;
        }

        [Transaction]
        public void DeleteUnit(int unitId)
        {
            var unit = UnitRepository.GetById(unitId);
            UnitRepository.Delete(unit);
        }

        [Transaction]
        public FunctionalUnit Update(FunctionalUnit unit)
        {
            this.UnitRepository.Update(unit);
            return unit;

        }
    }
}
