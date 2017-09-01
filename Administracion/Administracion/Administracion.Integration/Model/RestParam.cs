using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Model
{
    public class RestParam
    {
        public RestParam(string Name, object Value, RestParamType Type = RestParamType.GetOrPost)
        {
            this.Name = Name;
            this.Value = Value;
            this.Type = Type;
        }
        public string Name { get; set; }
        public object Value { get; set; }
        public RestParamType Type { get; set; }
    }
}
