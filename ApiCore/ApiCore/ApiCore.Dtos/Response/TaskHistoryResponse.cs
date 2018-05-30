using ApiCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class TaskHistoryResponse
    {
        public virtual int Id {get; set;}
        public virtual string Coment { get; set; }        
        public virtual DateTime FollowDate { get; set; }
        
    }
}
