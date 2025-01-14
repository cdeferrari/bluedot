﻿using Administracion.DomainModel;
using Administracion.Dto.Owner;
using Administracion.Dto.Provider;
using Administracion.Dto.Renter;
using Administracion.Dto.Worker;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Administrations;
using Administracion.Services.Contracts.FunctionalUnits;
using Administracion.Services.Contracts.Owners;
using Administracion.Services.Contracts.Ownerships;
using Administracion.Services.Contracts.PaymentTypesService;
using Administracion.Services.Contracts.Providers;
using Administracion.Services.Contracts.Renters;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Contracts.Workers;
using Administracion.Services.Implementations.Tickets;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Helpers;
using WebGrease.Css.Extensions;

namespace Administracion.Controllers
{
    public class UsersController : Controller
    {
        public virtual IUserService UserService { get; set; }
        public virtual IFunctionalUnitService FunctionalUnitService { get; set; }
        public virtual IWorkerService WorkerService { get; set; }
        public virtual IOwnerService OwnerService { get; set; }
        public virtual IOwnershipService OwnershipService { get; set; }
        public virtual IRenterService RenterService { get; set; }
        public virtual IProviderService ProviderService { get; set; }
        public virtual IPaymentTypesService PaymentTypeService { get; set; }
        public virtual IAdministrationService AdministrationService { get; set; }

        public ActionResult Index()
        {
            try
            {
                //var workers = this.WorkerService.GetAll();
                var owners = this.OwnerService.GetAll();
                //var renters = this.RenterService.GetAll();


                //var usersIds = workers.Select(x => x.User.Id)
                //    .Union(owners.Select(x => x.User.Id))
                //    .Union(renters.Select(x => x.User.Id))
                //    .ToList();

                //var users = this.UserService.GetAll().Select(x => !usersIds.Contains(x.Id)).ToList();

                var paymentTypes = this.PaymentTypeService.GetAll().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                });

                var usersViewModel = new List<UserViewModel>();

                //workers.ForEach(x => usersViewModel.Add(
                //    new UserViewModel()
                //    {
                //        Id = x.User.Id,
                //        CUIT = x.User.CUIT,
                //        ContactData = Mapper.Map<ContactDataViewModel>(x.User.ContactData),
                //        DNI = x.User.DNI,
                //        Name = x.User.Name,
                //        Surname = x.User.Surname,
                //        IsWorker = true, 
                //        PaymentTypes = paymentTypes
                //    }));

                owners.ForEach(x => usersViewModel.Add(
                    new UserViewModel()
                    {
                        Id = x.User.Id,
                        CUIT = x.User.CUIT,
                        ContactData = Mapper.Map<ContactDataViewModel>(x.User.ContactData),
                        DNI = x.User.DNI,
                        Name = x.User.Name,
                        Surname = x.User.Surname,
                        IsOwner = true,
                        PaymentTypes = paymentTypes
                    }));

                //renters.ForEach(x => usersViewModel.Add(
                //    new UserViewModel()
                //    {
                //        Id = x.User.Id,
                //        CUIT = x.User.CUIT,
                //        ContactData = Mapper.Map<ContactDataViewModel>(x.User.ContactData),
                //        DNI = x.User.DNI,
                //        Name = x.User.Name,
                //        Surname = x.User.Surname,
                //        IsRenter = true,
                //        PaymentTypes = paymentTypes
                //    }));


                return View("List", usersViewModel);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        // GET: Backlog
        [HttpGet]
        public ActionResult CreateUser()
        {
            var administrations = new List<SelectListItem>();
            var paymentTypes = new List<SelectListItem>();
            var ownershipList = new List<SelectListItem>();

            if (AdministrationService.GetAll() != null)
            {
                administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
            }

            if (PaymentTypeService.GetAll() != null)
            {
                paymentTypes = this.PaymentTypeService.GetAll().Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Description
                }).ToList();
            }

            var ownerships = OwnershipService.GetAll();


            if (ownerships.Count > 0)
            {
                ownerships.ForEach(x =>
                {
                    if(x.Address != null)
                    {
                        ownershipList.Add(new SelectListItem()
                        {
                            Value = x.Id.ToString(),
                            Text = x.Address.Street + " " + x.Address.Number
                        });
                    }
                });

                ownershipList.OrderBy(x => x.Text).ToList();
            }

            var functionalUnitList = new List<FunctionalUnit>().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street + " " + x.Ownership.Address.Number
            });

            var userViewModel = new UserViewModel() { Administrations = administrations, PaymentTypes = paymentTypes, OwnershipList = ownershipList, FunctionalUnitList = functionalUnitList, IsOwner = true };
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateUser(UserViewModel user)
        {

            var nuser = new User();

            try
            {
                nuser = Mapper.Map<User>(user);

                if (user.Id == 0)
                {
                    nuser = this.UserService.CreateUser(nuser);

                    if (user.IsOwner)
                    {
                        var owner = new OwnerRequest()
                        {
                            UserId = nuser.Id,
                            FunctionalUnitIds = user.Units != null ? user.Units : new List<int>(),
                            PaymentTypeId = user.PaymentTypeId
                        };
                        this.OwnerService.CreateOwner(owner);
                    }

                    if (user.IsProvider)
                    {
                        var provider = new ProviderRequest()
                        {
                            UserId = nuser.Id
                        };
                        this.ProviderService.CreateProvider(provider);
                    }

                    if (user.IsRenter)
                    {
                        var renter = new RenterRequest()
                        {
                            UserId = nuser.Id,
                            PaymentTypeId = user.PaymentTypeId,
                            FunctionalUnitId = user.FunctionalUnitId
                        };
                        this.RenterService.CreateRenter(renter);
                    }

                    if (user.IsWorker)
                    {
                        var worker = new WorkerRequest()
                        {
                            UserId = nuser.Id,
                            AdministrationId = user.AdministrationId
                        };
                        this.WorkerService.CreateWorker(worker);
                    }


                }
                else
                {
                    this.UserService.UpdateUser(nuser);
                    var owners = this.OwnerService.GetAll();
                    var ownersUsersIds = owners.Select(x => x.User.Id).ToList();

                    var renters = this.RenterService.GetAll();
                    var rentersUsersIds = renters.Select(x => x.User.Id).ToList();

                    var workers = this.WorkerService.GetAll();
                    var workersUsersIds = workers.Select(x => x.User.Id).ToList();

                    if (user.IsOwner)
                    {

                        var owner = new OwnerRequest()
                        {
                            UserId = nuser.Id,
                            PaymentTypeId = user.PaymentTypeId,
                            FunctionalUnitIds = user.Units
                        };

                        if (ownersUsersIds.Contains(user.Id))
                        {
                            var oldOwner = owners.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            owner.Id = oldOwner.Id;
                            this.OwnerService.UpdateOwner(owner);
                        }
                        else
                        {
                            this.OwnerService.CreateOwner(owner);
                        }

                    }
                    else
                    {
                        if (ownersUsersIds.Contains(user.Id))
                        {
                            var owner = owners.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            this.OwnerService.DeleteOwner(owner.Id);
                        }

                    }

                    if (user.IsRenter)
                    {

                        var renter = new RenterRequest()
                        {
                            UserId = nuser.Id,
                            PaymentTypeId = user.PaymentTypeId,
                            FunctionalUnitId = user.FunctionalUnitId
                        };

                        if (rentersUsersIds.Contains(user.Id))
                        {
                            var oldRenter = renters.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            renter.Id = oldRenter.Id;
                            this.RenterService.UpdateRenter(renter);
                        }
                        else
                        {
                            this.RenterService.CreateRenter(renter);
                        }

                    }
                    else
                    {
                        if (rentersUsersIds.Contains(user.Id))
                        {
                            var renter = renters.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            this.RenterService.DeleteRenter(renter.Id);
                        }

                    }

                    if (user.IsWorker)
                    {

                        var worker = new WorkerRequest()
                        {
                            UserId = nuser.Id
                        };

                        if (workersUsersIds.Contains(user.Id))
                        {
                            var oldWorker = workers.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            worker.Id = oldWorker.Id;
                            this.WorkerService.UpdateWorker(worker);
                        }
                        else
                        {
                            this.WorkerService.CreateWorker(worker);
                        }

                    }
                    else
                    {
                        if (rentersUsersIds.Contains(user.Id))
                        {
                            var renter = renters.Where(x => x.User.Id.Equals(user.Id)).FirstOrDefault();
                            this.RenterService.DeleteRenter(renter.Id);
                        }

                    }

                }

                if (!string.IsNullOrEmpty(user.CallbackUrl))
                {
                    return Redirect(user.CallbackUrl);
                }
                else
                {
                    return Redirect("/Users/Index");
                }

            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }


        public ActionResult UpdateUserById(int id, string callbackUrl)
        {
            var oUser = this.UserService.GetUser(id);
            var user = Mapper.Map<UserViewModel>(oUser);
            var renters = this.RenterService.GetAll();

            var administrations = this.AdministrationService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            var ownershipList = this.OwnershipService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Address.Street + " " + x.Address.Number
            });

            user.OwnershipList = ownershipList;
            user.FunctionalUnitList = new List<FunctionalUnit>().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Ownership.Address.Street + " " + x.Ownership.Address.Number
            });

            user.Administrations = administrations;

            var paymentTypes = this.PaymentTypeService.GetAll().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Description
            });

            user.PaymentTypes = paymentTypes;

            var renterUsers = renters.Select(x => x.User.Id).ToList();
            if (renterUsers.Contains(id))
            {
                var renter = renters.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                user.PaymentTypeId = renter.PaymentTypeId;
                user.FunctionalUnitId = renter.FunctionalUnitId;
                user.IsRenter = true;
            }
            else
            {
                var owners = this.OwnerService.GetAll();
                var owner = owners.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                user.PaymentTypeId = owner.PaymentTypeId;
                user.IsOwner = true;


                if (owner.FunctionalUnitId.Count > 0)
                {
                    var functionalUnits = this.FunctionalUnitService.GetAll();

                    var userUnit = functionalUnits.Where(x => owner.FunctionalUnitId.Contains(x.Id)).FirstOrDefault();

                    user.OwnershipId = userUnit.Ownership.Id;

                    user.FunctionalUnitList = functionalUnits.Where(x => x.Ownership.Id == userUnit.Ownership.Id).Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = "Nro:" + x.Number + " Piso:" + x.Floor + " Dto:" + x.Dto
                    });

                    user.FunctionalUnitUserList = Mapper.Map<List<FunctionalUnitViewModel>>(functionalUnits.Where(x => owner.FunctionalUnitId.Contains(x.Id)).ToList());

                    user.FunctionalUnitId = userUnit.Id;
                }

            }

            user.CallbackUrl = callbackUrl;

            return View("CreateUser", user);
        }

        public ActionResult UpdateUser(UserViewModel user)
        {
            var nuser = new User();

            nuser = Mapper.Map<User>(user);
            this.UserService.UpdateUser(nuser);
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserViewModel user)
        {
            User currUser = this.UserService.GetUser(user.Id);
            if (!string.IsNullOrEmpty(user.Name)) { currUser.Name = user.Name; }
            if (!string.IsNullOrEmpty(user.Surname)) { currUser.Surname = user.Surname; }
            if (!string.IsNullOrEmpty(user.DNI)) { currUser.DNI = user.DNI; }
            if (!string.IsNullOrEmpty(user.CUIT)) { currUser.CUIT = user.CUIT; }
            var photo = WebImage.GetImageFromRequest("ProfilePic");
            if (photo != null)
            {
                string fileExtension = Path.GetExtension(photo.FileName);
                string newFileName = "user-" + user.Id + fileExtension;
                string imgPath = "Images/" + newFileName;
                photo.Save(@"~/" + imgPath, null, false);
                currUser.ProfilePic = newFileName;
            }
            //if (!string.IsNullOrEmpty(user.ProfilePic)) { currUser.ProfilePic = user.ProfilePic; }
            //User Contact
            if (user.ContactData != null) //Si se cambio algo en la ContactData
            {//Se puede asumir que currUser tiene ContactData porque sino no aparece el form
                if (!string.IsNullOrEmpty(user.ContactData.Email))
                {
                    currUser.ContactData.Email = user.ContactData.Email;
                }
                if (!string.IsNullOrEmpty(user.ContactData.Telephone))
                {
                    currUser.ContactData.Telephone = user.ContactData.Telephone;
                }
                if (!string.IsNullOrEmpty(user.ContactData.Cellphone))
                {
                    currUser.ContactData.Cellphone = user.ContactData.Cellphone;
                }
            }

            bool result = this.UserService.UpdateUser(currUser);
            if (result)
            {
                //Updateamos la info en SessionPersister.Account
                SessionPersister.Account.UserName = currUser.Name + " " + currUser.Surname;
                SessionPersister.Account.User.ProfilePic = currUser.ProfilePic;
                SessionPersister.Account.User.Name = currUser.Name ?? "";
                SessionPersister.Account.User.Surname = currUser.Surname ?? "";
                if (currUser.ContactData != null) { SessionPersister.Account.Email = currUser.ContactData.Email; }

                DateTime dt = DateTime.Now;
                //La idea de mandar la date.now es que se refresque el cache y se vea si hubo un cambio en las fotos
                return Redirect("/Users/Details?" + dt);
            }
            else
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult DeleteUser(int id)
        {
            var workers = this.WorkerService.GetAll();
            var owners = this.OwnerService.GetAll();
            var renters = this.RenterService.GetAll();

            var workerUsers = workers.Select(x => x.User.Id).ToList();
            var ownerUsers = owners.Select(x => x.User.Id).ToList();
            var renterUsers = renters.Select(x => x.User.Id).ToList();

            if (workerUsers.Contains(id))
            {
                var worker = workers.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.WorkerService.DeleteWorker(worker.Id);
            }

            if (ownerUsers.Contains(id))
            {
                var owner = owners.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.OwnerService.DeleteOwner(owner.Id);
            }

            if (renterUsers.Contains(id))
            {
                var renter = renters.Where(x => x.User.Id.Equals(id)).FirstOrDefault();
                this.RenterService.DeleteRenter(renter.Id);
            }

            this.UserService.DeleteUser(id);
            return Redirect("/Users/Index");
        }

        public ActionResult List()
        {

            try
            {
                var users = this.UserService.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }

        public ActionResult Details()
        {
            int id = SessionPersister.Account.User.Id;
            var oUser = this.UserService.GetUser(id);
            var user = Mapper.Map<UserViewModel>(oUser);

            return View(user);
        }

        [HttpGet]
        public ActionResult BaseUserEdit(int? id)
        {
            BaseUserEditViewModel model = new BaseUserEditViewModel();
            if (id.HasValue)
            {
                User user = this.UserService.GetUser(id.Value);
                model.SetUser(user);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult BaseUserEdit(BaseUserEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                User user = new DomainModel.User();
                ContactData contactData = new ContactData();
                if (model.Id != 0)
                {
                    user = this.UserService.GetUser(model.Id);
                }
                model.GetUser(ref user);
                model.GetContactData(ref contactData);
                user.ContactData = contactData;
                if (model.ProfilePic != null)
                {
                    user.ProfilePic = GetUserImageName(model.ProfilePic, user.Id);
                    SaveUserImage(model.ProfilePic, user.ProfilePic);
                }
                if (user.Id != 0)
                {
                    UserService.UpdateUser(user);
                }
                else
                {
                    UserService.CreateUser(user);
                }
                if (SessionPersister.Account.User.Id == user.Id)
                {
                    UpdateLoggedUser(user);
                }
                return View("/Users/BaseUserDetails/" + user.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult BaseUserList()
        {
            try
            {
                List<User> users = this.UserService.GetAll().ToList();
                return View(users);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult BaseUserDetails(int id)
        {
            try
            {
                User user = this.UserService.GetUser(id);
                if (user == null)
                {
                    return View("../Shared/Error");
                }
                UserViewModel model = Mapper.Map<UserViewModel>(user);
                return View(model);
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
        }

        public string GetUnitsByOwnership(int id)
        {

            var functionalUnitList = this.OwnershipService.GetUnits(id).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = "Nro:" + x.Number + " Piso:" + x.Floor + " Dto:" + x.Dto
            });

            return JsonConvert.SerializeObject(functionalUnitList);

        }

        private string GetUserImageName(HttpPostedFileBase image, int userId)
        {
            string fileExtension = Path.GetExtension(image.FileName);
            string newFileName = "user-" + userId + fileExtension;
            return newFileName;
        }

        private void SaveUserImage(HttpPostedFileBase image, string name)
        {
            string imgPath = Path.Combine(Server.MapPath("~/Images"), name);
            image.SaveAs(imgPath);
        }

        private void UpdateLoggedUser(User user)
        {
            SessionPersister.Account.UserName = user.Name + " " + user.Surname;
            SessionPersister.Account.User.ProfilePic = user.ProfilePic;
            SessionPersister.Account.User.Name = user.Name ?? "";
            SessionPersister.Account.User.Surname = user.Surname ?? "";
            if (user.ContactData != null) { SessionPersister.Account.Email = user.ContactData.Email; }
        }

    }
}