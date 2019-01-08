using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Dto.Bill;
using Administracion.Dto.Spend;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Bills;
using Administracion.Services.Contracts.Consortiums;
using Administracion.Services.Contracts.ExpensesBill;
using Administracion.Services.Contracts.Managers;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.SpendClass;
using Administracion.Services.Contracts.SpendItemsService;
using Administracion.Services.Contracts.Spends;
using Administracion.Services.Contracts.SpendTypes;
using Administracion.Services.Contracts.Workers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public virtual ISpendClassService SpendClassService { get; set; }
        public virtual ISpendItemsService SpendItemService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IManagerService ManagerService { get; set; }
        public virtual IExpensesBillervice ExpensesBillService { get; set; }

        private decimal suterhPercentage = decimal.Parse(ConfigurationManager.AppSettings["suterhPercentage"]);
        private decimal fateryhPercentage = decimal.Parse(ConfigurationManager.AppSettings["fateryhPercentage"]);
        private decimal seracarhPercentage = decimal.Parse(ConfigurationManager.AppSettings["seracarhPercentage"]);

        // GET: Backlog
        [HttpGet]
        public ActionResult ChoseConsortium()
        {
            var consortiums = this.ConsortiumService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street + " " + x.Ownership.Address.Number
            }).OrderBy(x => x.Text);


            var choseConsortiumVM = new ChoseConsortiumViewModel()
            {
                Consortiums = consortiums
            };

            return View(choseConsortiumVM);

        }

        [HttpPost]
        public ActionResult ChoseConsortium(ChoseConsortiumViewModel choseConsortium)
        {
            return Redirect("/Spend/Index?Id=" + choseConsortium.ConsortiumId);

        }

        public ActionResult Index(int id)
        {
            try
            {
                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                var endDate = new DateTime(startDate.Year, startDate.Month, startDate.AddMonths(1).AddDays(-1).Day, 0, 0, 0);
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

                var salaryDictionary = new Dictionary<int,IList<Spend>>();

                var spendItemDetYSueldos = spendItemsList.Where(x => x.Text.ToLower().Equals("detalle de sueldo y cargas sociales")).FirstOrDefault();

                var salarySpends = spendsList.Where(x => x.Type.Item.Id == int.Parse(spendItemDetYSueldos.Value)).ToList();

                var spendsWithoutManager = new List<Spend>();

                var managers = new List<Manager>();

                var aportesYcontribucionesSpends = new Dictionary<string,decimal>();

                salarySpends.ForEach(x => 
                {
                    if (x.Bill.Manager != null)
                    {
                        if(x.Description == "Suterh" || x.Description == "Fateryh" || x.Description == "Seracarh")
                        {
                            if (!aportesYcontribucionesSpends.Keys.Contains(x.Description))
                            {
                                aportesYcontribucionesSpends.Add(x.Description, x.Bill.Amount);
                            }
                            else
                            {
                                var actual = aportesYcontribucionesSpends[x.Description];
                                actual += x.Bill.Amount;
                                aportesYcontribucionesSpends[x.Description] = actual;
                            }
                        }
                        else
                        {
                            if (!salaryDictionary.Keys.Contains(x.Bill.Manager.Id))
                            {
                                var list = new List<Spend>() { x };
                                salaryDictionary.Add(x.Bill.Manager.Id, list);
                                managers.Add(x.Bill.Manager);

                            }
                            else
                            {
                                var list = salaryDictionary[x.Bill.Manager.Id];
                                list.Add(x);
                                salaryDictionary[x.Bill.Manager.Id] = list;
                            }
                        }
                        
                    }
                    else
                    {
                        spendsWithoutManager.Add(x);
                    }    
                });

                var salaryDictionaryManager = new Dictionary<Manager, IList<Spend>>();

                foreach (var key in salaryDictionary.Keys)
                {
                    salaryDictionaryManager.Add(managers.Where(x => x.Id == key).FirstOrDefault(), salaryDictionary[key]);
                }

                var spendsViewModel = new SpendViewModel()
                {
                    Id = id,
                    Month = startDate.Month,
                    Spends = spendsList,
                    SpendItems = spendItemsList,
                    SpendTypes = spendTypesList,
                    ConsortiumId = id,
                    SalarySpends = salaryDictionaryManager,
                    AportesYContribucionesSpends = aportesYcontribucionesSpends,
                    SalarySpendWithoutManager = spendsWithoutManager
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

            var spendClasses = this.SpendClassService.GetAll()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

            List<Provider> providerList = this.ProviderService.GetAll().ToList();
            providerList.Sort((x, y) => string.Compare(x.User.Name, y.User.Name));

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
                SpendClass = new SelectList(spendClasses, "Value", "Text"),
                Managers = new SelectList(managers, "Value", "Text"),
                Providers = new SelectList(providerList, "Id", "User.Name"),
                Workers = new SelectList(workers, "Value", "Text"),
                ConsortiumId = consortiumId,
                SpendItemId = spendItemId,
                TaskId = taskId
            };

            return View(viewModel);
        }

        public ActionResult DeleteList(int consortiumId, int managerId)
        {
            var startDate = DateTime.Now.AddDays(-DateTime.Now.Date.Day);
            var endDate = DateTime.Now.AddDays(30 - DateTime.Now.Date.Day);
            var spendsList = this.SpendService.GetByConsortiumId(consortiumId, startDate, endDate);

            List<Spend> listToDelete;
            if (managerId == 0)
            {
                listToDelete = spendsList.Where(x => x.Bill.Manager == null).ToList();
            }
            else
            {
                listToDelete = spendsList.Where(x => x.Bill.Manager != null && x.Bill.Manager.Id == managerId).ToList();
            }

            listToDelete.ForEach(x => this.SpendService.DeleteSpend(x.Id));

            return Redirect(string.Format("/Spend/Index?Id={0}", consortiumId));
        }

        [HttpGet]
        public ActionResult UpdateSpendById(int id)
        {
            var spend = this.SpendService.GetSpend(id);

            var spendClasses = this.SpendClassService.GetAll()
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });


            var spendTypes = this.SpendTypeService.GetAll().Where(x => x.Item.Id == spend.Type.Item.Id && x.Consortium.Id == spend.Consortium.Id)
                .Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

            List<Provider> providerList = this.ProviderService.GetAll().ToList();
            providerList.Sort((x, y) => string.Compare(x.User.Name, y.User.Name));

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
                SpendClassId = spend.SpendClass.Id,
                SpendTypes = new SelectList(spendTypes, "Value", "Text"),
                SpendClass = new SelectList(spendClasses, "Value", "Text"),
                Managers = new SelectList(managers, "Value", "Text"),
                Providers = new SelectList(providerList, "Id", "User.Name"),
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
                ClientNumber = spend.Bill.ClientNumber,
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
                SpendClassId = spend.SpendClassId,
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

        public ActionResult PastListMenu(int id)
        {
            var model = new CreateSpendViewModel()
            {
                ConsortiumId = id
            };
            return View(model);
        }

        public ActionResult PastList(int id, int pastMonth)
        {
            try
            {
                var startDate = DateTime.Now.AddMonths(-pastMonth);
                startDate = startDate.AddDays(-startDate.Day);

                var endDate = DateTime.Now.AddMonths(-pastMonth);
                endDate = endDate.AddDays(30 - endDate.Day);
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

                var spendsWithoutManager = new List<Spend>();
                var salaryDictionary = new Dictionary<int, IList<Spend>>();
                var managers = new List<Manager>();
                var spendItemDetYSueldos = spendItemsList.Where(x => x.Text.ToLower().Equals("detalle de sueldo y cargas sociales")).FirstOrDefault();

                var salarySpends = spendsList.Where(x => x.Type.Item.Id == int.Parse(spendItemDetYSueldos.Value)).ToList();

                salarySpends.ForEach(x =>
                {
                    if (x.Bill.Manager != null)
                    {
                        if (!salaryDictionary.Keys.Contains(x.Bill.Manager.Id))
                        {
                            var list = new List<Spend>() { x };
                            salaryDictionary.Add(x.Bill.Manager.Id, list);
                            managers.Add(x.Bill.Manager);

                        }
                        else
                        {
                            var list = salaryDictionary[x.Bill.Manager.Id];
                            list.Add(x);
                            salaryDictionary[x.Bill.Manager.Id] = list;
                        }
                    }
                    else
                    {
                        spendsWithoutManager.Add(x);
                    }
                });

                var salaryDictionaryManager = new Dictionary<Manager, IList<Spend>>();

                foreach (var key in salaryDictionary.Keys)
                {
                    salaryDictionaryManager.Add(managers.Where(x => x.Id == key).FirstOrDefault(), salaryDictionary[key]);
                }
                

                var spendsViewModel = new SpendViewModel()
                {
                    Spends = spendsList,
                    SpendItems = spendItemsList,
                    SpendTypes = spendTypesList,
                    ConsortiumId = id,
                    SalarySpends = salaryDictionaryManager,
                    SalarySpendWithoutManager = spendsWithoutManager
                };

                return View("List", spendsViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }


        }

        [HttpPost]
        public ActionResult ProcessManagerCsv(int consortiumId)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var spendDictionary = new Dictionary<string, IList<Spend>>();
                    StreamReader csvreader = new StreamReader(file.InputStream);
                    var spendTypes = this.SpendTypeService.GetAll();
                    var spendClasses = this.SpendClassService.GetAll();
                    var managers = this.ManagerService.GetAll();
             
                    while (!csvreader.EndOfStream)
                    {

                        var line = csvreader.ReadLine();
                        var values = line.Split(';').ToList();
                        
                        this.ParseSpend(values, spendDictionary, managers, spendTypes, spendClasses, consortiumId);

                    }

                    CreateLaboralUnionSpends(spendDictionary, managers, consortiumId,spendTypes,spendClasses);

                    spendDictionary.Keys.ToList().ForEach(x => this.PostSpend(spendDictionary[x], consortiumId));
                }
            }

            return Redirect(string.Format("/Spend/Index?Id={0}", consortiumId));
        }

        [HttpGet]
        public FileResult PrintExpensesPDF(int id, int month)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var spendsList = SpendService.GetByConsortiumId(id, startDate, endDate);
            var consortium = ConsortiumService.GetConsortium(id);

            var expensesHtml = ExpensesBillService.GetExpensesBill(consortium, spendsList, month);
            return File(ExpensesBillService.GetPDFTickets(expensesHtml), "application/pdf");
        }

        [HttpGet]
        public ContentResult PrintExpensesHtml(int id, int month)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var spendsList = SpendService.GetByConsortiumId(id, startDate, endDate);
            var consortium = ConsortiumService.GetConsortium(id);

            var expensesHtml = ExpensesBillService.GetExpensesBill(consortium, spendsList, month);
            return Content("<style>" + expensesHtml.HtmlExpensesStyles + "</style>\n" + expensesHtml.HtmlExpenses);
        }

        private void CreateLaboralUnionSpends(IDictionary<string,IList<Spend>> spendsDictionary, IList<Manager> managers, int consortiumId, IList<SpendType> spendTypes, IList<SpendClass> spendClasses)
        {
            var spendsByCUIT = this.ParseSpendsDictionary(spendsDictionary);
            
            foreach (var key in spendsByCUIT.Keys)
            {

                var keyWithoutSimbols = key.Trim().Replace("-", "");
                var manager = managers.Where(x => x.User!= null && !string.IsNullOrEmpty(x.User.CUIT) &&  x.User.CUIT.Trim().Replace("-", "") == keyWithoutSimbols).FirstOrDefault();
                if (manager != null)
                {
                    var spends = spendsByCUIT[key];
                    var spendsSum = spends.Sum(x =>
                    {
                        if (x.SpendClass.Id == 4)
                        {
                            return x.Bill.Amount;
                        }
                        else
                        {
                            return 0;
                        }
                    });

                    spendsSum = decimal.Round(spendsSum, 0);

                    var suterhSpend = this.CreateSuterhSpend(spendsSum, manager, consortiumId, spendTypes, spendClasses);
                    var fateryhSpend = this.CreateFateryhSpend(spendsSum, manager, consortiumId, spendTypes, spendClasses);
                    var seracarhSpend = this.CreateSeracarhSpend(spendsSum, manager, consortiumId, spendTypes, spendClasses);

                    spendsDictionary.Add(suterhSpend.FirstOrDefault().Description + "_" + manager.User.CUIT + "_" + suterhSpend.FirstOrDefault().PaymentDate.ToString(), suterhSpend);
                    spendsDictionary.Add(fateryhSpend.FirstOrDefault().Description + "_" + manager.User.CUIT + "_" + fateryhSpend.FirstOrDefault().PaymentDate.ToString(), fateryhSpend);
                    spendsDictionary.Add(seracarhSpend.FirstOrDefault().Description + "_" + manager.User.CUIT + "_" + seracarhSpend.FirstOrDefault().PaymentDate.ToString(), seracarhSpend);
                }                
            }
        }

        private IList<Spend> CreateSuterhSpend(decimal spendsSum,Manager manager, int consortiumId, IList<SpendType> spendTypes, IList<SpendClass> spendClasses)
        {
            var spendDescription = ConfigurationManager.AppSettings["SuterhSpend"];
            var amount = spendsSum * suterhPercentage / 100;
            var result = new List<Spend>()
            {
                CreateSpend(spendDescription, null, amount.ToString(), consortiumId, DateTime.Now, spendTypes, spendClasses.Where(x => x.Description == "D").FirstOrDefault(), manager)
                    
            };
            
            return result;
        }

        private IList<Spend> CreateFateryhSpend(decimal spendsSum, Manager manager, int consortiumId, IList<SpendType> spendTypes, IList<SpendClass> spendClasses)
        {
            var spendDescription = ConfigurationManager.AppSettings["FateryhSpend"];
            var amount = spendsSum * fateryhPercentage / 100;

            //cambiar la condicion, debe ser por medio tiempo o tiempo completo
            if (manager.IsAlternate)
            {
                amount += 75;
            }
            else
            {
                amount += 150;
            }

            var result = new List<Spend>()
            {
                CreateSpend(spendDescription, null, amount.ToString(), consortiumId, DateTime.Now, spendTypes, spendClasses.Where(x => x.Description == "D").FirstOrDefault(), manager)

            };

            return result;
        }

        private IList<Spend> CreateSeracarhSpend(decimal spendsSum, Manager manager, int consortiumId, IList<SpendType> spendTypes, IList<SpendClass> spendClasses)
        {
            var spendDescription = ConfigurationManager.AppSettings["SeracarhSpend"];
            var amount = spendsSum * seracarhPercentage / 100;
            var result = new List<Spend>()
            {
                CreateSpend(spendDescription, null, amount.ToString(), consortiumId, DateTime.Now, spendTypes, spendClasses.Where(x => x.Description == "D").FirstOrDefault(), manager)

            };
            return result;
        }

        private IDictionary<string,IList<Spend>> ParseSpendsDictionary(IDictionary<string, IList<Spend>> spendsDictionary)
        {
            var result = new Dictionary<string, IList<Spend>>();

            foreach (var key in spendsDictionary.Keys)
            {
                var spends = spendsDictionary[key];                
                var cuit = key.Split('_')[1];
                var resultSpends = result.Keys.Contains(cuit) ? result[cuit] : new List<Spend>();

                if(resultSpends.Count()>0)
                {
                    resultSpends = resultSpends.Concat(spends).ToList();
                }
                else
                {
                    resultSpends = spends;
                }
                result[cuit] = resultSpends;
            }            
            return result;
        }

        private void ParseSpend(IList<string> values, Dictionary<string, IList<Spend>> dictionary, IList<Manager> managers, IList<SpendType> spendTypes,IList<SpendClass> spendClasses, int consortiumId)
        {
            var code = values[88];
            if (code != "00")
            {
                
                var managerCuit = values[25];
                var cuitWithouthSimbols = managerCuit.Trim().Replace("-", "");
                var manager = managers.Where(x => !string.IsNullOrEmpty(x.User.CUIT) && x.User.CUIT.Trim().Replace("-", "") == cuitWithouthSimbols).FirstOrDefault();
                var spendDate = DateTime.Now;

                var spendDescription = values[89];
                if (!string.IsNullOrEmpty(spendDescription))
                {
                    var spendData = values[90];
                    var spendClassD = values[91];
                    var spendClassE = values[92];

                    var spendsToAdd = new List<Spend>();
                    if (!string.IsNullOrEmpty(spendClassD) && spendClassD != "0")
                    {
                        spendsToAdd.Add(this.CreateSpend(spendDescription, spendData, spendClassD, consortiumId, spendDate, spendTypes, spendClasses.Where(x => x.Description == "D").FirstOrDefault(), manager));
                    }

                    if (!string.IsNullOrEmpty(spendClassE) && spendClassE != "0")
                    {
                        spendsToAdd.Add(this.CreateSpend(spendDescription, spendData, spendClassE, consortiumId, spendDate, spendTypes, spendClasses.Where(x => x.Description == "E").FirstOrDefault(), manager));
                    }

                    try
                    {
                        dictionary.Add(spendDescription + "_" + managerCuit + "_" + spendDate, spendsToAdd);
                    }
                    catch(Exception ex)
                    {

                        System.Threading.Thread.Sleep(1000);
                        spendDate = DateTime.Now;
                        dictionary.Add(spendDescription + "_" + managerCuit + "_" + spendDate, spendsToAdd);
                    }
                }
                                
            }
            

        }



        private Spend CreateSpend(string description,string data, string value, int consortiumId, DateTime spendDate, IList<SpendType> spendTypes, SpendClass spendClass, Manager manager)            
        {
            var spendType = spendTypes.Where(x => x.Consortium.Id == consortiumId && x.Description == description).FirstOrDefault();

            var result = new Spend()
            {
                Bill = new Bill()
                {
                    Amount = decimal.Parse(value),
                    Manager = manager
                },
                Consortium = new Consortium() { Id = consortiumId },
                Description = string.IsNullOrEmpty(data) ? description : description + " "+data+"%",              
                PaymentDate = DateTime.Now,// spendDate,
                SpendClass = spendClass,
                Type = spendType != null ? spendType : spendTypes.Where(x => x.Consortium.Id == consortiumId && x.Description == "otro").FirstOrDefault()
        };
            return result;

        }

        private void PostSpend(IList<Spend> spends, int consortiumId)
        {
            foreach (var spend in spends)
            {
                var nbill = new BillRequest()
                {
                    Amount = spend.Bill.Amount,
                    CreationDate = spend.PaymentDate,
                    Number = "00000",
                    ExpirationDate = spend.PaymentDate,
                    NextExpirationDate = spend.PaymentDate,
                    ManagerId = spend.Bill.Manager != null ? spend.Bill.Manager.Id : 0
                };

                var nspend = new SpendRequest()
                {
                    Description = spend.Description,
                    PaymentDate = spend.PaymentDate,
                    SpendTypeId = spend.Type.Id,
                    SpendClassId = spend.SpendClass.Id,
                    ConsortiumId = consortiumId
                    
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
               

                    if (result)
                    {
                        nspend.BillId = entity.Id;
                        result = this.SpendService.CreateSpend(nspend);

                    }
                }
                catch (Exception ex)
                {

                }

            }

        }
    }
}