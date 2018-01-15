using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Models
{
    public class FastUpdateViewModel
    {
        public virtual int Id { get; set; }
        public virtual Address Address { get; set; }
        public virtual string MailingList { get; set; }
        public virtual string Telephone { get; set; }
    }
}