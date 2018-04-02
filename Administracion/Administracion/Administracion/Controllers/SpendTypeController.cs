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
using Newtonsoft.Json;
using System.Web.Services;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class SpendTypeController : Controller
    {
        public virtual ISpendTypeService SpendTypeservice { get; set; }
        public virtual IUserService UserService { get; set; }
        
        public string CreateSpendType(int consortiumId, int spendItemId, string description, bool required, bool forAll)
        {

            var spendType = new SpendTypeViewModel()
            {
                ConsortiumId = consortiumId,
                Description = description,
                ForAll = forAll,
                Required = required,
                SpendItemId = spendItemId
            };

            var nSpendTypes = Mapper.Map<SpendTypeRequest>(spendType);                        

            try
            {
             
                var entity = this.SpendTypeservice.CreateSpendType(nSpendTypes);
                var result = entity.Id != 0;                
                if (result)
                {
                    return JsonConvert.SerializeObject(result);
                    //return Redirect(string.Format("/Spend/CreateSpend?consortiumId={0}&spendItemId={1}", SpendType.ConsortiumId, SpendType.SpendItemId));
                }
                else
                {
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex);
            }
        }


        
        public ActionResult DeleteSpendTypes(int id, int consortiumId)
        {                    
            this.SpendTypeservice.DeleteSpendType(id);

            return Redirect(string.Format("/Spend/Index?consortiumId={0}", consortiumId));
        }



        public string GetByConsortium(int id, int itemId)
        {
            var spendlist = this.SpendTypeservice.GetAll();

            var spendTypesList = spendlist.Where(x=> x.Consortium.Id == id && x.Item.Id == itemId).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            return JsonConvert.SerializeObject(spendTypesList);

        }

    }
}
