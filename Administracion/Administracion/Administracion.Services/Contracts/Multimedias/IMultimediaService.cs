using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Multimedias
{
    public interface IMultimediaService
    {
     //   List<Multimedia> GetAll();
        Multimedia GetMultimedia(int MultimediaId);
        bool CreateMultimedia(Multimedia Multimedia);
       // bool UpdateMultimedia(Multimedia Multimedia);
       // bool DeleteMultimedia(int MultimediaId);
    }
}
