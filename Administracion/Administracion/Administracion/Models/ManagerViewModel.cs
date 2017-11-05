using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class ManagerViewModel
    {
    	public virtual UserViewModel User{ get; set; }
        public virtual IEnumerable<SelectListItem> LaboralUnionList { get; set; }
		public virtual int Id { get; set; }
        public virtual Address Home { get; set; }
        public virtual Address JobDomicile { get; set; }        
        public virtual DateTime StartDate { get; set; }
        public virtual int LaborUnionId { get; set; }
        public virtual double Salary { get; set; }
        public virtual string WorkInsurance { get; set; }
        public virtual bool IsAlternate { get; set; }

    }
}