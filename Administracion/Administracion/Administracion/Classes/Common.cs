using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administracion
{
    public class Common
    {
        public static string Capitalize(string text)
        {
            return text.Substring(0, 1).ToUpper() + text.Substring(1).ToLower();
        }
    }
}