using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class ContactDataViewModel
    {
        public virtual int Id { get; set; }
        [DisplayName("Telefono")]
        public virtual string Telephone { get; set; }
        [DisplayName("Celular")]
        public virtual string Cellphone { get; set; }
        public virtual string Email { get; set; }        

    }
}