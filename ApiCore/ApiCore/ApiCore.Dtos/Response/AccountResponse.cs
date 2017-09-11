using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class AccountResponse
    {
        public virtual string CBU { get; set; }
        public virtual AccountTypeResponse AccountType { get; set; }
        public virtual int Number { get; set; }
    }
}
