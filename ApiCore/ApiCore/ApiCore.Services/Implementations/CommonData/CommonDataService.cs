using ApiCore.Services.Contracts.CommonDatas;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Services.Contracts.CommonDatas;

namespace ApiCore.Services.Implementations.CommonDatas
{
    public class CommonDataService : ICommonDataService
    {
        public ICommonDataRepository CommonDataRepository { get; set; }
        public ICommonDataItemsRepository CommonDataItemRepository { get; set; }
        public IOwnershipRepository OwnershipRepository { get; set; }
 
        [Transaction]
        public CommonData CreateCommonData(CommonDataRequest CommonData)
        {
            var entityToInsert = new CommonData() { };
            MergeCommonData(entityToInsert, CommonData);
            CommonDataRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public CommonData GetById(int CommonDataId)
        {
            var CommonData = CommonDataRepository.GetById(CommonDataId);
            if (CommonData == null)
                throw new BadRequestException(ErrorMessages.PropiedadNoEncontrada);

            return CommonData;
        }
        

        [Transaction]
        public CommonData UpdateCommonData(CommonData originalCommonData, CommonDataRequest CommonData)
        {            
            this.MergeCommonData(originalCommonData, CommonData);
            CommonDataRepository.Update(originalCommonData);
            return originalCommonData;

        }
        

        [Transaction]
        public void DeleteCommonData(int CommonDataId)
        {
            var CommonData = CommonDataRepository.GetById(CommonDataId);
            CommonDataRepository.Delete(CommonData);
        }

        [Transaction]
        public IList<CommonData> GetAll()
        {
            var CommonDatas = CommonDataRepository.GetAll();
            if (CommonDatas == null)
                throw new BadRequestException(ErrorMessages.PropiedadNoEncontrada);

            var result = new List<CommonData>();
            var enumerator = CommonDatas.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }


        [Transaction]
        public IList<CommonData> GetByOwnership(int id)
        {
            var CommonDatas = CommonDataRepository.GetByOwnership(id);
            if (CommonDatas == null)
                throw new BadRequestException(ErrorMessages.PropiedadNoEncontrada);

            var result = new List<CommonData>();
            var enumerator = CommonDatas.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }
        
        

        private void MergeCommonData(CommonData originalCommonData, CommonDataRequest CommonData)
        {
            originalCommonData.Have  = CommonData.Have;           
            originalCommonData.Item = this.CommonDataItemRepository.GetById(CommonData.CommonDataItemId);
            originalCommonData.Ownership = this.OwnershipRepository.GetById(CommonData.OwnershipId);
            
        }
        
    }
}
