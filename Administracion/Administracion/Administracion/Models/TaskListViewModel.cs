using Administracion.DomainModel;
using Administracion.DomainModel.Enum;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class TaskListViewModel
    {
        public virtual int Id { get; set;   }
        public virtual bool IsSuccess { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int ResultId { get; set; }
        public virtual string Description { get; set; }
        public virtual string Coments { get; set; }
        public IEnumerable<SelectListItem> Results { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }

}
