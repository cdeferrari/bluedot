using System.Collections.Generic;
using Administracion.DomainModel;
using Administracion.Integration.Contracts;
using Administracion.Integration.Model;
using System.Configuration;
using Administracion.Library.ApiResources;
using Administracion.Services.Contracts.Multimedias;


namespace Administracion.Services.Implementations.Multimedias
{
    public class MultimediaService : IMultimediaService
    {
        public ISync IntegrationService { get; set; }

        public bool CreateMultimedia(Multimedia Multimedia)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.CreateMultimedia, RestMethod.Post, null, Multimedia);                        
        }

        public Multimedia GetMultimedia(int MultimediaId)
        {
            return IntegrationService.RestCall<Multimedia>(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.GetMultimedia, MultimediaId), RestMethod.Get, null, new RestParamList { new RestParam("id", MultimediaId) });
        }

        /*
        public bool UpdateMultimedia(Multimedia Multimedia)
        {            
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.UpdateMultimedia, Multimedia.Id), RestMethod.Put, null, Multimedia);
        }

        public bool DeleteMultimedia(int MultimediaId)
        {
            return IntegrationService.RestCallNoReturn(ConfigurationManager.AppSettings["ApiCoreUrl"], string.Format(ApiCore.DeleteMultimedia, MultimediaId), RestMethod.Delete, null, new RestParamList { new RestParam("id", MultimediaId) });                        
        }

        

        public List<Multimedia> GetAll()
        {
            return IntegrationService.RestCall<List<Multimedia>>(ConfigurationManager.AppSettings["ApiCoreUrl"], ApiCore.GetAllMultimedia, RestMethod.Get, null, null);
            
        }
        */
    }
}
