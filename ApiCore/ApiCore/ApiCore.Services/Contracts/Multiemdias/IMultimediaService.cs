using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Multimedias
{
    public interface IMultimediaService
    {
        List<Multimedia> GetAll();
        Multimedia CreateMultimedia(Multimedia Multimedia);
        Multimedia GetById(int MultimediaId);
        Multimedia UpdateMultimedia(Multimedia originalMultimedia, Multimedia Multimedia);
        void DeleteMultimedia(int MultimediaId);
    }
}
