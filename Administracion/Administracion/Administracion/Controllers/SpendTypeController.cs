using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.SpendTypes;
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
using Administracion.Dto.SpendType;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class SpendTypeController : Controller
    {
        public virtual ISpendTypeService SpendTypeservice { get; set; }
        public virtual IUserService UserService { get; set; }
        
        [HttpPost]
        public ActionResult CreateSpendType(SpendTypeViewModel SpendType)
        {
         
            var nSpendTypes = Mapper.Map<SpendTypeRequest>(SpendType);                        

            try
            {
             
                var entity = this.SpendTypeservice.CreateSpendType(nSpendTypes);
                var result = entity.Id != 0;                
                if (result)
                {
                    return Redirect(string.Format("/Spend/CreateSpend?consortiumId={0}&spendItemId={1}", SpendType.ConsortiumId, SpendType.SpendItemId));
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


        
        public ActionResult DeleteSpendTypes(int id, int consortiumId)
        {                    
            this.SpendTypeservice.DeleteSpendType(id);

            return Redirect(string.Format("/Spend/Index?consortiumId={0}", consortiumId));
        }
        
        
    }
}
