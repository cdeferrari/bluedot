using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class AdministrationResponse
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CUIT { get; set; }
        public virtual Address Address { get; set; }
        public virtual DateTime StartDate { get; set; }
    }
}
