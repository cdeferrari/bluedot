using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class UserViewModel
    {
        public virtual int Id { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public virtual string Name { get; set; }
        [Required]
        [DisplayName("Apellido")]
        public virtual string Surname { get; set; }
        public virtual string DNI { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string ProfilePic { get; set; }
        [DisplayName("Comentarios")]
        [DataType(DataType.MultilineText)]
        public virtual string Comments { get; set; }    
        public virtual ContactDataViewModel ContactData { get; set; }

        public virtual bool IsWorker { get; set; }
        public virtual bool IsOwner { get; set; }
        public virtual bool IsRenter { get; set; }
        public virtual bool IsProvider { get; set; }

        public virtual string CallbackUrl { get; set; }

        public virtual IEnumerable<SelectListItem> Administrations { get; set; }
        public virtual IEnumerable<SelectListItem> PaymentTypes { get; set; }
        public virtual IEnumerable<SelectListItem> OwnershipList { get; set; }
        public virtual IEnumerable<SelectListItem> FunctionalUnitList { get; set; }
        public virtual List<FunctionalUnitViewModel> FunctionalUnitUserList { get; set; }
        public virtual int AdministrationId { get; set; }
        public virtual int PaymentTypeId { get; set; }
        public virtual int FunctionalUnitId { get; set; }
        public virtual int OwnershipId { get; set; }
        public virtual List<int> Units { get; set; }

    }
}