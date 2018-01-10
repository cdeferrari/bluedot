using Administracion.DomainModel;
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
        public virtual IEnumerable<SelectListItem> ConsortiumList { get; set; }
        public virtual int Id { get; set; }
        public virtual AddressViewModel Home { get; set; }

        [DisplayName("Fecha de inicio")]
        public virtual DateTime StartDate { get; set; }
        [DisplayName("Fecha de nacimiento")]
        public virtual DateTime BirthDate { get; set; }
        public virtual int LaborUnionId { get; set; }
        public virtual int ConsortiumId { get; set; }
        [DisplayName("Salario")]
        public virtual double Salary { get; set; }
        [DisplayName("Seguro laboral")]
        public virtual string WorkInsurance { get; set; }
        public virtual bool IsAlternate { get; set; }
        public virtual bool Male { get; set; }

        [DisplayName("Valor hora extra")]
        public virtual double ExtraHourValue { get; set; }
        public virtual string ShirtWaist { get; set; }
        public virtual string PantsWaist { get; set; }
        public virtual string FootwearWaist { get; set; }
        [DisplayName("Horario")]
        public virtual string Schedule { get; set; }
    }
}