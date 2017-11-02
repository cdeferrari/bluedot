using Administracion.DomainModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Administracion.Models
{
    public class CheckListViewModel
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Customer { get; set; }
        public virtual DateTime OpenDate { get; set; }
        public virtual List<TaskListViewModel> Tasks { get; set; }
        public virtual string Coments { get; set; }
                         
    }

}
