using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.Bill;
using Administracion.Dto.Spend;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Bills;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.Managers;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.SpendItemsService;
using Administracion.Services.Contracts.Spends;
using Administracion.Services.Contracts.SpendTypes;
using Administracion.Services.Contracts.Workers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    
    [CustomAuthorize(Roles.All)]
    public class SpendController : Controller
    {
        public virtual IConsortiumService ConsortiumService { get; set; }
        public virtual IBillService BillService { get; set; }
        public virtual ISpendService SpendService { get; set; }        
        public virtual ISpendTypeService SpendTypeService { get; set; }
        public virtual ISpendItemsService SpendItemService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IManagerService ManagerService { get; set; }

        // GET: Backlog
        public ActionResult Index(int id)
        {
            try
            {
                var startDate = DateTime.Now.AddDays(-DateTime.Now.Date.Day);
                var endDate = DateTime.Now.AddDays(30 -DateTime.Now.Date.Day);
                var spendsList = this.SpendService.GetByConsortiumId(id, startDate, endDate);

                var spendTypes = this.SpendTypeService.GetAll();
                var spendItems = this.SpendItemService.GetAll();

                var spendTypesList = spendTypes.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

                var spendItemsList = spendItems.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });


                var spendsViewModel = new SpendViewModel()
                {
                    Spends = spendsList,
                    SpendItems = spendItemsList,
                    SpendTypes = spendTypesList,
                    ConsortiumId = id
                };
                                        
                return View("List", spendsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        [HttpGet]
        public ActionResult CreateSpend(int consortiumId, int spendItemId, int? taskId = null)
        {
            var consortium = this.ConsortiumService.GetConsortium(consortiumId);

            var spendTypes = this.SpendTypeService.GetAll().Where(x => x.Item.Id == spendItemId && x.Consortium.Id == consortiumId)                
                .Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });
            
            var providers = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name+" "+ x.User.Surname
            });

            var workers = this.WorkerService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var managers = consortium.Managers.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });


            var viewModel = new CreateSpendViewModel()
            {                
                SpendTypes = new SelectList(spendTypes, "Value", "Text"),
                Managers = new SelectList(managers, "Value", "Text"),
                Providers = new SelectList(providers, "Value", "Text"),
                Workers = new SelectList(workers, "Value", "Text"),
                ConsortiumId = consortiumId,
                SpendItemId = spendItemId,
                TaskId = taskId
            };
                        
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult UpdateSpendById(int id)
        {
            var spend = this.SpendService.GetSpend(id);            

            var spendTypes = this.SpendTypeService.GetAll().Where(x => x.Item.Id == spend.Type.Item.Id && x.Consortium.Id == spend.Consortium.Id)
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

            var providers = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var workers = this.WorkerService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });

            var managers = spend.Consortium.Managers.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name + " " + x.User.Surname
            });


            var viewModel = new CreateSpendViewModel()
            {
                Bill = spend.Bill,
                Description = spend.Description,
                ManagerId = spend.Bill.Manager != null ? spend.Bill.Manager.Id : 0,
                ProviderId = spend.Bill.Provider != null ? spend.Bill.Provider.Id : 0,
                WorkerId = spend.Bill.Worker != null ? spend.Bill.Worker.Id : 0,
                Id = spend.Id,
                SpendTypeId = spend.Type.Id,
                SpendTypes = new SelectList(spendTypes, "Value", "Text"),
                Managers = new SelectList(managers, "Value", "Text"),
                Providers = new SelectList(providers, "Value", "Text"),
                Workers = new SelectList(workers, "Value", "Text"),
                ConsortiumId = spend.Consortium.Id,
                SpendItemId = spend.Type.Item.Id
            };

            return View("CreateSpend", viewModel);
        }




        [HttpPost]
        public ActionResult CreateUpdateSpend(CreateSpendViewModel spend)
        {
            var nbill = new BillRequest()
            {
                Amount = spend.Bill.Amount,
                CreationDate = DateTime.Now,
                Number = spend.Bill.Number,
                ExpirationDate = spend.Bill.ExpirationDate,
                NextExpirationDate = spend.Bill.NextExpirationDate,
                ManagerId = spend.ManagerId,
                ProviderId = spend.ProviderId,
                WorkerId = spend.WorkerId,                
                Id = spend.Bill.Id
            };
            
            var nspend = new SpendRequest()
            {
                Description = spend.Description,
                PaymentDate = DateTime.Now,
                SpendTypeId = spend.SpendTypeId,                
                ConsortiumId = spend.ConsortiumId,
                TaskId = spend.TaskId,
                Id = spend.Id
            };
            

            try
            {
                var result = false;
                Entidad entity = new Entidad() { Id = nbill.Id };
                if (nbill.Id == 0)
                {
                    entity = this.BillService.CreateBill(nbill);
                    result = entity.Id != 0;
                }
                else
                {
                    result = this.BillService.UpdateBill(nbill);                    
                }

                if (result)
                {
                    if (nspend.Id == 0)
                    {
                        nspend.BillId = entity.Id;
                        result = this.SpendService.CreateSpend(nspend);
                    }
                    else
                    {
                        nspend.BillId = entity.Id;
                        result = this.SpendService.UpdateSpend(nspend);
                    }

                    if (result)
                    {
                        return Redirect(string.Format("/Spend/Index?Id={0}", spend.ConsortiumId));
                    }
                    else
                    {
                        return View("../Shared/Error");
                    }                    
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
        
        
        public ActionResult DeleteSpend(int id, int consortiumId)
        {            
            this.SpendService.DeleteSpend(id);
            return Redirect(string.Format("/Spend/Index?Id={0}", consortiumId));
        }
        

    }
}