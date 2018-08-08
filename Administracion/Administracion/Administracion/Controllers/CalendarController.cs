using Administracion.Security;
using Administracion.Services.Contracts.Tasks;
using Administracion.Services.Contracts.Tickets;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using System.Linq;
using System;

namespace Administracion.Controllers
{
    public class CalendarController : Controller
    {
        public virtual ITaskService TaskService { get; set; }
        public virtual ITicketService TicketService { get; set; }
        private const double MinHoursWarning = 72;
        private const double MinHoursDanger = 24;
        
        public ActionResult Index()
        {
            int currUserId = SessionPersister.Account.User.Id;
            Models.Calendar.Calendar model = new Models.Calendar.Calendar();
            List<Models.Calendar.Event> eventList = new List<Models.Calendar.Event>();

            IEnumerable<DomainModel.Ticket> openTickets = TicketService.GetAll().Where(x => x.Status.Description == "open");
            List<Models.TicketViewModel> myTickets = BacklogController.GetSelfTickets(currUserId, Mapper.Map<List<Models.TicketViewModel>>(openTickets));

            LoadEventsFromTicketList(ref eventList, myTickets);
            LoadEventsFromTicketTasks(ref eventList, myTickets);           

            model.EventList = eventList;

            return View(model);
        }

        public void LoadEventsFromTicketList(ref List<Models.Calendar.Event> eventList, List<Models.TicketViewModel> ticketList)
        {
            foreach (Models.TicketViewModel ticket in ticketList)
            {
                string url = Url.Action("UpdateTicketById", "Backlog", new { id = ticket.Id });
                if (ticket.LimitDate != null)
                {
                    double remainingHours = DateTimeDifferenceInHours(ticket.LimitDate, DateTime.Now);
                    string color = Models.Calendar.Color.Success;
                    if (remainingHours < MinHoursDanger)
                    {
                        color = Models.Calendar.Color.Danger;
                    }
                    else if (remainingHours < MinHoursWarning)
                    {
                        color = Models.Calendar.Color.Warning;
                    }

                    eventList.Add(new Models.Calendar.Event()
                    {
                        Title = ticket.Title,
                        Day = ticket.LimitDate.Day,
                        Month = ticket.LimitDate.Month - 1,
                        Year = ticket.LimitDate.Year,
                        Color = color,
                        Url = url,
                        Description = GetTicketEventDescription(ticket, false)
                    });
                }
                string description = GetTicketEventDescription(ticket, true);
                List<Models.TicketHistoryViewModel> ticketHistoryList = new List<Models.TicketHistoryViewModel>();
                if(ticket.TicketHistory != null)
                {
                    ticketHistoryList.AddRange(ticket.TicketHistory);
                }
                if(ticket.TicketFollow != null)
                {
                    ticketHistoryList.Add(ticket.TicketFollow);
                }
                LoadEventsFromTicketHistoryList(ref eventList, ticketHistoryList, url, description);
            }            
        }

        public void LoadEventsFromTicketHistoryList(ref List<Models.Calendar.Event> eventList, List<Models.TicketHistoryViewModel> ticketHistoryList, string url, string description)
        {
            foreach (Models.TicketHistoryViewModel ticketHistory in ticketHistoryList)
            {
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = ticketHistory.Coment,
                    Day = ticketHistory.FollowDate.Day,
                    Month = ticketHistory.FollowDate.Month - 1,
                    Year = ticketHistory.FollowDate.Year,
                    Color = Models.Calendar.Color.Ticket,
                    Description = description,
                    Url = url
                });
            }
        }

        public void LoadEventsFromTaskList(ref List<Models.Calendar.Event> eventList, IEnumerable<DomainModel.Task> taskList, string ticketTitle)
        {
            foreach(DomainModel.Task task in taskList)
            {
                string url = Url.Action("Details", "Task", new { id = task.Id });
                string description = GetTaskEventDescription(task, ticketTitle);
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = task.Description,
                    Day = task.OpenDate.Day,
                    Month = task.OpenDate.Month - 1,
                    Year = task.OpenDate.Year,
                    Color = Models.Calendar.Color.Task,
                    Description = description,
                    Url = url
                });

                LoadEventsFromTaskHistoryList(ref eventList, task.TaskHistory, url, description);
            }
        }

        public void LoadEventsFromTicketTasks(ref List<Models.Calendar.Event> eventList, List<Models.TicketViewModel> ticketList)
        {
            foreach (Models.TicketViewModel ticket in ticketList)
            {
                LoadEventsFromTaskList(ref eventList, ticket.Tasks.Where(x => x.Status.Description == "open"), ticket.Title);
            }
            
        }

        public void LoadEventsFromTaskHistoryList(ref List<Models.Calendar.Event> eventList, IList<DomainModel.TaskHistory> taskHistoryList, string url, string description)
        {
            foreach(DomainModel.TaskHistory taskHistory in taskHistoryList)
            {
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = taskHistory.Coment,
                    Day = taskHistory.FollowDate.Day,
                    Month = taskHistory.FollowDate.Month - 1,
                    Year = taskHistory.FollowDate.Year,
                    Color = Models.Calendar.Color.Task,
                    Description = description,
                    Url = url
                });
            }
        }        

        private double DateTimeDifferenceInHours(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date1 - date2;
            return difference.TotalHours;
        }

        private string GetTicketEventDescription(Models.TicketViewModel ticket, bool ticketTitle)
        {
            string description = "";
            if(ticketTitle)
            {
                description += "<label>Titulo ticket</label>" +
                "<p>" + ticket.Title + "</p>";
            }
            description +=
            "<label>Descripción</label>" +
                "<p>" + ticket.Description + "</p>";

            return description;
        }

        private string GetTaskEventDescription(DomainModel.Task task, string ticketTitle)
        {
            string description = 
                "<label>Prioridad</label>" +
                "<p>" + task.Priority.Description + "</p>" +
                "<label>Ticket padre</label>" +
                "<p>" + ticketTitle + "</p>";
            if (task.Provider != null && task.Provider.User != null)
            {
                description += "<label>Proveedor</label>" +
                "<p>" + task.Provider.User.Name + "</p>";
            }

            return description;
        }

    }
}