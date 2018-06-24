using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class TaskDetailsViewModel
    {
        public DomainModel.Task Task { get; set; }
        public List<DomainModel.SpendItem> SpendItemList { get; set; }
        public int SpendItemId { get; set; }
    }
}