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
    public class SpendTypeViewModel
    {
        public virtual int Id { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual int SpendItemId { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Required { get; set; }
        public virtual bool ForAll { get; set; }

    }
}