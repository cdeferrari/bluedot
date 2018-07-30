using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models.Calendar
{
    public class Calendar
    {
        public List<Event> EventList { get; set; } = new List<Event>();
    }

    public class Event
    {
        public string Title { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Url { get; set; }
    }
}