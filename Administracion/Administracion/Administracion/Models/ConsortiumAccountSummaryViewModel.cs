using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion.Models
{
    public class ConsortiumAccountSummaryViewModel
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public IList<UnitAccountStatusSummary> Units { get; set; }
    }
}