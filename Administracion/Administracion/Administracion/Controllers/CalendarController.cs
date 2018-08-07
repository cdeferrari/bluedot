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
                if (ticket.LimitDate != null)//PUEDE QUE ESTEN FALTANDO TICKET OPEN Y TICKET CLOSE
                {
                    eventList.Add(new Models.Calendar.Event() {
                        Title = ticket.Title,
                        Day = ticket.LimitDate.Day,
                        Month = ticket.LimitDate.Month - 1,
                        Year = ticket.LimitDate.Year,
                        Color = Models.Calendar.Color.TicketOpen,
                        Url = url
                    });
                }
                List<Models.TicketHistoryViewModel> ticketHistoryList = new List<Models.TicketHistoryViewModel>();
                if(ticket.TicketHistory != null)
                {
                    ticketHistoryList.AddRange(ticket.TicketHistory);
                }
                if(ticket.TicketFollow != null)
                {
                    ticketHistoryList.Add(ticket.TicketFollow);
                }
                LoadEventsFromTicketHistoryList(ref eventList, ticketHistoryList, url);
            }            
        }

        public void LoadEventsFromTicketTasks(ref List<Models.Calendar.Event> eventList, List<Models.TicketViewModel> ticketList)
        {
            List<DomainModel.Task> taskList = new List<DomainModel.Task>();
            foreach (Models.TicketViewModel ticket in ticketList)
            {
                taskList.AddRange(ticket.Tasks.Where(x => x.Status.Description == "open"));
            }
            LoadEventsFromTaskList(ref eventList, taskList);
        }

        public void LoadEventsFromTaskList(ref List<Models.Calendar.Event> eventList, List<DomainModel.Task> taskList)
        {
            foreach(DomainModel.Task task in taskList)
            {
                string url = Url.Action("Details", "Task", new { id = task.Id });
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = task.Description,
                    Day = task.OpenDate.Day,
                    Month = task.OpenDate.Month - 1,
                    Year = task.OpenDate.Year,
                    Color = Models.Calendar.Color.TaskOpen,
                    Url = url
                });

                if(task.CloseDate.HasValue)
                {
                    eventList.Add(new Models.Calendar.Event()
                    {
                        Title = task.Description,
                        Day = task.CloseDate.Value.Day,
                        Month = task.CloseDate.Value.Month - 1,
                        Year = task.CloseDate.Value.Year,
                        Color = Models.Calendar.Color.TaskClose,
                        Url = url
                    });
                }

                LoadEventsFromTaskHistoryList(ref eventList, task.TaskHistory, url);
            }
        }

        public void LoadEventsFromTaskHistoryList(ref List<Models.Calendar.Event> eventList, IList<DomainModel.TaskHistory> taskHistoryList, string url)
        {
            foreach(DomainModel.TaskHistory taskHistory in taskHistoryList)
            {
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = taskHistory.Coment,
                    Day = taskHistory.FollowDate.Day,
                    Month = taskHistory.FollowDate.Month - 1,
                    Year = taskHistory.FollowDate.Year,
                    Color = Models.Calendar.Color.TaskFollow,
                    Url = url
                });
            }
        }

        public void LoadEventsFromTicketHistoryList(ref List<Models.Calendar.Event> eventList, List<Models.TicketHistoryViewModel> ticketHistoryList, string url)
        {
            foreach(Models.TicketHistoryViewModel ticketHistory in ticketHistoryList)
            {
                eventList.Add(new Models.Calendar.Event()
                {
                    Title = ticketHistory.Coment,
                    Day = ticketHistory.FollowDate.Day,
                    Month = ticketHistory.FollowDate.Month - 1,
                    Year = ticketHistory.FollowDate.Year,
                    Color = Models.Calendar.Color.TicketFollow,
                    Url = url
                });
            }
        }

        private double DateTimeDifferenceInHours(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date1 - date2;
            return difference.TotalHours;
        }
    }
}