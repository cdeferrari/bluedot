using Administracion.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Income
{
    public class IncomeTypeRequest
    {
        public virtual int Id { get; set; }
        public virtual bool Required { get; set; }
        public virtual int ConsortiumId { get; set; }
        public virtual string Description { get; set; }

    }
}
