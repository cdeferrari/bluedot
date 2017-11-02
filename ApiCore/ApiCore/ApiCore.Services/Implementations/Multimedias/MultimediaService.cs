using ApiCore.Services.Contracts.Tickets;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using ApiCore.Services.Contracts.Multimedias;
using AutoMapper;
using System.Collections.Generic;

namespace ApiCore.Services.Implementations.Multimedias
{
    public class MultimediaService : IMultimediaService
    {
        
        public IMultimediaRepository MultimediaRepository { get; set; }                

        [Transaction]
        public Multimedia CreateMultimedia(Multimedia Multimedia)
        {
            Multimedia originalMultimedia = new Multimedia();
            var entityToInsert = MergeMultimedia(originalMultimedia, Multimedia);

            MultimediaRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public Multimedia GetById(int MultimediaId)
        {
            var Multimedia = MultimediaRepository.GetById(MultimediaId);
            if (Multimedia == null)
                throw new BadRequestException(ErrorMessages.ConsorcioNoEncontrado);

            return Multimedia;
        }
        

        [Transaction]
        public Multimedia UpdateMultimedia(Multimedia originalMultimedia, Multimedia Multimedia)
        {            
            originalMultimedia = this.MergeMultimedia(originalMultimedia, Multimedia);
            MultimediaRepository.Update(originalMultimedia);
            return originalMultimedia;

        }
        

        [Transaction]
        public void DeleteMultimedia(int MultimediaId)
        {
            var Multimedia = MultimediaRepository.GetById(MultimediaId);
            MultimediaRepository.Delete(Multimedia);
        }
        

        private Multimedia MergeMultimedia(Multimedia originalMultimedia, Multimedia Multimedia)
        {                       
            originalMultimedia.MultimediaTypeId = Multimedia.MultimediaTypeId;
            originalMultimedia.OwnershipId = Multimedia.OwnershipId;
            originalMultimedia.Url = Multimedia.Url;
           
            return originalMultimedia;
        }

        public List<Multimedia> GetAll()
        {
            var Multimedias = MultimediaRepository.GetAll();
            if (Multimedias == null)
                throw new BadRequestException(ErrorMessages.ConsorcioNoEncontrado);

            var result = new List<Multimedia>();
            var enumerator = Multimedias.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result;
        }
    }
}
