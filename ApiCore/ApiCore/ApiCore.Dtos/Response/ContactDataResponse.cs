using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Dtos.Response
{
    public class ContactDataResponse
    {
        public virtual string Telephone { get; set; }
        public virtual string Cellphone { get; set; }
        public virtual string Email { get; set; }
    }
}
