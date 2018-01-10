using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administracion.Services.Contracts.LaboralUnion;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Dto.ConsortiumSecure;
using Administracion.Services.Contracts.TaskResult;
using Administracion.Services.Contracts.SecureStatus;
using Administracion.Services.Contracts.ConsortiumSecure;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class ConsortiumSecureController : Controller
    {
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IConsortiumSecureService ConsortiumSecureService { get; set; }
        public virtual ISecureStatusService SecureStatusService { get; set; }

        

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateConsortiumSecure(int id)
        {
            var consortium = this.ConsortiumService.GetConsortium(id);
            

            var secureStatusList = this.SecureStatusService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var viewModel = new ConsortiumSecureViewModel()
            {
                ConsortiumId = consortium.Id,
                SecureStatus = secureStatusList               
            };

            return View(viewModel);            
        }

        [HttpPost]
        public ActionResult CreateUpdateConsortiumSecure(ConsortiumSecureViewModel consortiumSecure)
        {
         
            var consortium = this.ConsortiumService.GetConsortium(consortiumSecure.ConsortiumId);            
            var nsecure = Mapper.Map<ConsortiumSecureRequest>(consortiumSecure);                        

            try
            {                
                var result = false;
                if (nsecure.Id == 0)
                {                    
                    var entity = this.ConsortiumSecureService.CreateConsortiumSecure(nsecure);
                    result = entity.Id != 0;                    
                }
                else
                {             
                    result = this.ConsortiumSecureService.UpdateConsortiumSecure(nsecure);                    
                }
                
                if (result)
                {
                    return Redirect(string.Format( "/Consortium/Details/{0}",consortium.Id));
                }
                else
                {
                    return View("../Shared/Error");
                }
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }


        public ActionResult UpdateConsortiumSecureById(int id, int consortiumId)
        {
            var oConsortiumSecure = this.ConsortiumSecureService.GetConsortiumSecure(id);
            var secureStatusList = this.SecureStatusService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            var ConsortiumSecure = Mapper.Map<ConsortiumSecureViewModel>(oConsortiumSecure);
            ConsortiumSecure.ConsortiumId = consortiumId;
            ConsortiumSecure.SecureStatusId = oConsortiumSecure.Status.Id;
            ConsortiumSecure.SecureStatus = secureStatusList;
            return View("CreateConsortiumSecure",ConsortiumSecure);
        }

        public ActionResult UpdateConsortiumSecure(ConsortiumSecureViewModel ConsortiumSecure)
        {       
            var nConsortiumSecure = Mapper.Map<ConsortiumSecureRequest>(ConsortiumSecure);            
            this.ConsortiumSecureService.UpdateConsortiumSecure(nConsortiumSecure);
            return View();
        }

        public ActionResult DeleteConsortiumSecure(int id, int consortiumId)
        {                    
            var ConsortiumSecure = this.ConsortiumSecureService.GetConsortiumSecure(id);

            this.ConsortiumSecureService.DeleteConsortiumSecure(id);
            
            return Redirect(string.Format("/Consortium/Details/{0}", consortiumId));
        }
        
        public ActionResult List()
        {                  
            try
            {
                var ConsortiumSecures = this.ConsortiumSecureService.GetAll();
                return View(ConsortiumSecures);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }

    }
}
