using Administracion.Security;
using Administracion.Services.Contracts.Tasks;
using Administracion.Services.Contracts.Tickets;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using System.Linq;

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
            IEnumerable<DomainModel.Ticket> openTickets = TicketService.GetAll().Where(x => x.Status.Description == "open");
            List<Models.TicketViewModel> myTickets = BacklogController.GetSelfTickets(currUserId, Mapper.Map<List<Models.TicketViewModel>>(openTickets));

            model.EventList.AddRange(GetEventsFromTickets(myTickets));

            return View(model);
        }

        public List<Models.Calendar.Event> GetEventsFromTickets(List<Models.TicketViewModel> ticketList)
        {
            List<Models.Calendar.Event> eventList = new List<Models.Calendar.Event>();
            foreach (Models.TicketViewModel ticket in ticketList)
            {
                string url = Url.Action("UpdateTicketById", "Backlog", new { id = ticket.Id });
                if (ticket.LimitDate != null)
                {
                    eventList.Add(new Models.Calendar.Event() {
                        Title = ticket.Title,
                        Day = ticket.LimitDate.Day,
                        Month = ticket.LimitDate.Month - 1,
                        Year = ticket.LimitDate.Year,
                        Color = Models.Calendar.Color.Default,
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
            return eventList;
        }

        public List<Models.Calendar.Event> GetEventsFromTasks(List<DomainModel.Task> taskList)
        {
            List<Models.Calendar.Event> eventList = new List<Models.Calendar.Event>();



            return eventList;
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
                    Color = Models.Calendar.Color.Default,
                    Url = url
                });
            }
        }
    }
}