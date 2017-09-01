using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Integration.Model
{
    public static class RestMaps
    {

        public static readonly Dictionary<RestParamType, ParameterType> ParamTypesMap = new Dictionary<RestParamType, ParameterType>
        {
            { RestParamType.Cookie,      ParameterType.Cookie      },
            { RestParamType.GetOrPost,   ParameterType.GetOrPost   },
            { RestParamType.UrlSegment,  ParameterType.UrlSegment  },
            { RestParamType.HttpHeader,  ParameterType.HttpHeader  },
            { RestParamType.RequestBody, ParameterType.RequestBody },
            { RestParamType.QueryString, ParameterType.QueryString }
        };

        public static readonly Dictionary<RestMethod, Method> MethodsMap = new Dictionary<RestMethod, Method>
        {
            { RestMethod.Get,     Method.GET     },
            { RestMethod.Post,    Method.POST    },
            { RestMethod.Put,     Method.PUT     },
            { RestMethod.Delete,  Method.DELETE  },
            { RestMethod.Head,    Method.HEAD    },
            { RestMethod.Options, Method.OPTIONS },
            { RestMethod.Patch,   Method.PATCH   },
            { RestMethod.Merge,   Method.MERGE   }
        };
    }
}
