using Administracion.DomainModel;
using Administracion.Models;
using Administracion.Services.Contracts.Tickets;
using Administracion.Services.Contracts.Users;
using Administracion.Services.Implementations.Tickets;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Controllers
{
    public class UsersController : Controller
    {
        public virtual IUserService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Backlog
        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult CreateNewUser(UserViewModel user)
        {
         
            var nuser = new User(); 
            
            nuser = Mapper.Map<User>(user);
            try
            {
                this.UserService.CreateUser(nuser);
                return View("CreateSuccess");
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }
            
        }


        public ActionResult UpdateUserById(int id)
        {
            var oUser = this.UserService.GetUser(id);
            var user = Mapper.Map<UserViewModel>(oUser);            
            return View("CreateUser",user);
        }

        public ActionResult UpdateUser(UserViewModel user)
        {            
            var nuser = new User();
            
            nuser = Mapper.Map<User>(user);            
            this.UserService.UpdateUser(nuser);
            return View();
        }

        public ActionResult DeleteUser(int id)
        {                    
            this.UserService.DeleteUser(id);
            return View();
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


    }
}