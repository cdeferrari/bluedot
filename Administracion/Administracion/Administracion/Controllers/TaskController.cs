using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using Administracion.Models;
using Administracion.Security;
using Administracion.Services.Contracts.Multimedias;
using Administracion.Services.Contracts.Messages;
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
using Administracion.Dto.Message;
using Administracion.Services.Contracts.Tasks;
using Administracion.Services.Contracts.Status;
using Administracion.Dto.Task;

namespace Administracion.Controllers
{
    [CustomAuthorize(Roles.All)]
    public class TaskController : Controller
    {
        public virtual ITaskService TasksService { get; set; }
        public virtual IUserService UserService { get; set; }
        public virtual IStatusService StatusService { get; set; }

        public ActionResult List()
        {
            List<Task> tasks = this.TasksService.GetAll().ToList();
            //List<TaskViewModel> tasksViewModel = Mapper.Map<List<TaskViewModel>>(tasks);
            TasksViewModel model = new TasksViewModel() { Tasks = tasks };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTask(TaskViewModel task)
        {
            var statusList = this.StatusService.GetAll();

            var ntask = new TaskRequest()
            {
                OpenDate = DateTime.Now,
                CreatorId = SessionPersister.Account.Id,
                Description = task.Description,
                StatusId = statusList.Where(x => x.Description.Equals("open")).FirstOrDefault().Id,
                PriorityId = task.PriorityId,
                WorkerId = task.Worker != null ? task.Worker.Id : 0,
                ProviderId = task.Provider != null ? task.Provider.Id : 0,
                ManagerId = task.Manager != null ? task.Manager.Id : 0,
                TicketId = task.TicketId                
            };
            
            try
            {
            
                var entity = this.TasksService.CreateTask(ntask);
                var result = entity.Id != 0;                
                if (result)
                {
                    return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", task.TicketId));
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

        [HttpPost]
        public ActionResult CreateTaskFollow(TaskHistoryViewModel task)
        {
            try
            {
                var taskHistoryRequest = Mapper.Map<TaskHistoryRequest>(task);
                this.TasksService.CreateTaskHistory(taskHistoryRequest);
                var otask = this.TasksService.GetTask(task.TaskId);
                return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", otask.Ticket.Id));
            }
            catch (Exception ex)
            {
                return View("../Shared/Error");
            }

        }


        public ActionResult DeleteTask(int id, int ticketId)
        {                    
            this.TasksService.DeleteTask(id);

            return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", ticketId));
        }


        //public ActionResult CreateSpend(int id, int ticketId)
        //{
        //    this.Messageservice.DeleteMessage(id);

        //    return Redirect(string.Format("/Backlog/UpdateTicketById/{0}", ticketId));
        //}


    }
}
