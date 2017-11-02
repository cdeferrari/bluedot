using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Request
{
    public class ManagerRequest
    {        
        public virtual Address Home { get; set; }
        public virtual Address JobDomicile { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual int LaborUnionId { get; set; }
        public virtual double Salary { get; set; }
        public virtual string WorkInsurance { get; set; }
        public virtual bool IsAlternate { get; set; }
        public virtual int ConsortiumId { get; set; }
        
    }
}
