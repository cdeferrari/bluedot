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

        // GET: Backlog
        [HttpGet]
        public ActionResult ChoseConsortium()
        {
            var consortiums = this.ConsortiumService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street + " " + x.Ownership.Address.Number
            });


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
                var startDate = DateTime.Now.AddDays(-DateTime.Now.Date.Day);
                var endDate = DateTime.Now.AddDays(30 - DateTime.Now.Date.Day);
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

            var providers = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name
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
                SpendClass = new SelectList(spendClasses, "Value", "Text"),
                Managers = new SelectList(managers, "Value", "Text"),
                Providers = new SelectList(providers, "Value", "Text"),
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

            var providers = this.ProviderService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.User.Name
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
                SpendClassId = spend.SpendClass.Id,
                SpendTypes = new SelectList(spendTypes, "Value", "Text"),
                SpendClass = new SelectList(spendClasses, "Value", "Text"),
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
                    //var keys = Congi

                    while (!csvreader.EndOfStream)
                    {

                        var line = csvreader.ReadLine();
                        var values = line.Split(';').ToList();

                        this.ParseSpend(values, spendDictionary, managers, spendTypes, spendClasses, consortiumId);

                        //var sueldoBasicoIndex = values.IndexOf("Sueldo Basico");
                        //if (sueldoBasicoIndex != -1 && !spendDictionary.Keys.Contains("Sueldo Basico"))
                        //{

                        //    var sueldoBasicoValue = values[sueldoBasicoIndex + 2];
                        //    spendDictionary.Add("Sueldo Basico", new List<Spend>() { this.CreateSpend("Sueldo Basico", sueldoBasicoValue, consortiumId, spendTypes) });
                        //}

                        //var retiroResiduosIndex = values.IndexOf("Retiro Residuos");
                        //if (retiroResiduosIndex != -1 && !spendDictionary.Keys.Contains("Retiro Residuos"))
                        //{
                        //    var retiroResiduosValue1 = values[retiroResiduosIndex + 1];
                        //    var retiroResiduosValue2 = values[retiroResiduosIndex + 2];
                        //    spendDictionary.Add("Retiro Residuos", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Retiro Residuos", retiroResiduosValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Retiro Residuos", retiroResiduosValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var plusMovimientoCochesIndex = values.IndexOf("Plus Movimiento Coches");
                        //if (plusMovimientoCochesIndex != -1 && !spendDictionary.Keys.Contains("Plus Movimiento Coches"))
                        //{
                        //    var plusMovimientoCochesValue = values[plusMovimientoCochesIndex + 2];
                        //    spendDictionary.Add("Plus Movimiento Coches", new List<Spend>() { this.CreateSpend("Plus Movimiento Coches", plusMovimientoCochesValue, consortiumId, spendTypes) });
                        //}


                        //var vacacionesIndex = values.IndexOf("Vacaciones");
                        //if (vacacionesIndex != -1 && !spendDictionary.Keys.Contains("Vacaciones"))
                        //{
                        //    var VacacionesValue1 = values[vacacionesIndex + 1];
                        //    var VacacionesValue2 = values[vacacionesIndex + 2];
                        //    spendDictionary.Add("Vacaciones", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Vacaciones", VacacionesValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Vacaciones", VacacionesValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var dtovacacionesIndex = values.IndexOf("Dcto Dias Vacaciones");
                        //if (dtovacacionesIndex != -1 && !spendDictionary.Keys.Contains("Dcto Dias Vacaciones"))
                        //{
                        //    var DtoVacacionesValue1 = values[dtovacacionesIndex + 1];
                        //    var DtoVacacionesValue2 = values[dtovacacionesIndex + 2];
                        //    spendDictionary.Add("Dcto Dias Vacaciones", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Dcto Dias Vacaciones", DtoVacacionesValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Dcto Dias Vacaciones", DtoVacacionesValue2, consortiumId, spendTypes)
                        //    });
                        //}


                        //var hsExtraIndex = values.IndexOf("Horas Extras 100%");
                        //if (hsExtraIndex != -1 && !spendDictionary.Keys.Contains("Horas Extras 100%"))
                        //{
                        //    var hsExtraValue1 = values[hsExtraIndex + 1];
                        //    var hsExtraValue2 = values[hsExtraIndex + 2];
                        //    spendDictionary.Add("Horas Extras 100%", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Horas Extras 100%", hsExtraValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Horas Extras 100%", hsExtraValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var hsFeriadoIndex = values.IndexOf("Horas Feriados");
                        //if (hsFeriadoIndex != -1 && !spendDictionary.Keys.Contains("Horas Feriados"))
                        //{
                        //    var hsFeriadoValue1 = values[hsFeriadoIndex + 1];
                        //    var hsFeriadoValue2 = values[hsFeriadoIndex + 2];
                        //    spendDictionary.Add("Horas Feriados", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Horas Feriados", hsFeriadoValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Horas Feriados", hsFeriadoValue2, consortiumId, spendTypes)
                        //    });
                        //}


                        //var jubilacionIndex = values.IndexOf("Jubilación");
                        //if (jubilacionIndex != -1 && !spendDictionary.Keys.Contains("Jubilación"))
                        //{
                        //    var jubilacionValue1 = values[jubilacionIndex + 1];
                        //    var jubilacionValue2 = values[jubilacionIndex + 3];
                        //    spendDictionary.Add("Jubilación", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Jubilación", jubilacionValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Jubilación", jubilacionValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var leyIndex = values.IndexOf("Ley 19032");
                        //if (leyIndex != -1 && !spendDictionary.Keys.Contains("Ley 19032"))
                        //{
                        //    var leyValue1 = values[leyIndex + 1];
                        //    var leyValue2 = values[leyIndex + 3];
                        //    spendDictionary.Add("Ley 19032", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Ley 19032", leyValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Ley 19032", leyValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var ObraSocialIndex = values.IndexOf("Obra Social OSPERYHRA");
                        //if (ObraSocialIndex != -1 && !spendDictionary.Keys.Contains("Obra Social OSPERYHRA"))
                        //{
                        //    var ObraSocialValue1 = values[ObraSocialIndex + 1];
                        //    var ObraSocialValue2 = values[ObraSocialIndex + 3];
                        //    spendDictionary.Add("Obra Social OSPERYHRA", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Obra Social OSPERYHRA", ObraSocialValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Obra Social OSPERYHRA", ObraSocialValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var CuotaSindicalIndex = values.IndexOf("Cuota Sindical SUTERH");
                        //if (CuotaSindicalIndex != -1 && !spendDictionary.Keys.Contains("Cuota Sindical SUTERH"))
                        //{
                        //    var CuotaSindicalValue1 = values[CuotaSindicalIndex + 1];
                        //    var CuotaSindicalValue2 = values[CuotaSindicalIndex + 3];
                        //    spendDictionary.Add("Cuota Sindical SUTERH", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Cuota Sindical SUTERH", CuotaSindicalValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Cuota Sindical SUTERH", CuotaSindicalValue2, consortiumId, spendTypes)
                        //    });
                        //}

                        //var CajaIndex = values.IndexOf("Caja Protecc. Fam. SUTERH");
                        //if (CajaIndex != -1 && !spendDictionary.Keys.Contains("Caja Protecc. Fam. SUTERH"))
                        //{
                        //    var CajalValue1 = values[CajaIndex + 1];
                        //    var CajalValue2 = values[CajaIndex + 3];
                        //    spendDictionary.Add("Caja Protecc. Fam. SUTERH", new List<Spend>()
                        //    {
                        //        this.CreateSpend("Caja Protecc. Fam. SUTERH", CajalValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("Caja Protecc. Fam. SUTERH", CajalValue2, consortiumId, spendTypes)
                        //    });
                        //}


                        //var FATERYHIndex = values.IndexOf("F.M.V.D.D. - FATERYH");
                        //if (FATERYHIndex != -1 && !spendDictionary.Keys.Contains("F.M.V.D.D. - FATERYH"))
                        //{
                        //    var FATERYHIValue1 = values[FATERYHIndex + 1];
                        //    var FATERYHIValue2 = values[FATERYHIndex + 3];
                        //    spendDictionary.Add("F.M.V.D.D. - FATERYH", new List<Spend>()
                        //    {
                        //        this.CreateSpend("F.M.V.D.D. - FATERYH", FATERYHIValue1, consortiumId, spendTypes),
                        //        this.CreateSpend("F.M.V.D.D. - FATERYH", FATERYHIValue2, consortiumId, spendTypes)
                        //    });
                        //}

                    }

                    spendDictionary.Keys.ToList().ForEach(x => this.PostSpend(spendDictionary[x], consortiumId));
                }
            }

            return Redirect(string.Format("/Spend/Index?Id={0}", consortiumId));
        }


        private void ParseSpend(IList<string> values, Dictionary<string, IList<Spend>> dictionary, IList<Manager> managers, IList<SpendType> spendTypes,IList<SpendClass> spendClasses, int consortiumId)
        {
            var code = values[88];
            if (code != "00")
            {
                
                var managerCuit = values[25];
                var cuitWithouthSimbols = managerCuit.Trim().Replace("-", "");
                var manager = managers.Where(x => !string.IsNullOrEmpty(x.User.CUIT) && x.User.CUIT.Trim().Replace("-", "") == cuitWithouthSimbols).FirstOrDefault();
                var spendDate = DateTime.Parse(values[39]);

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

                    dictionary.Add(spendDescription + "-" + managerCuit + "-" + spendDate, spendsToAdd);
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