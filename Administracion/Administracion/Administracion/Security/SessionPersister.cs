
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Administracion.Models;
using System.Configuration;

namespace Administracion.Security
{
    public static class SessionPersister
    {
        private static string usernameSessionvar = "username";

        public static AccountViewModel Account
        {
            get 
            {
                HttpContext.Current.Session.Timeout = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["sessionTimeOut"]) ? int.Parse(ConfigurationManager.AppSettings["sessionTimeOut"]) : 60;
                if (HttpContext.Current == null)
                    return null;

                var sessionVar = HttpContext.Current.Session[usernameSessionvar];

                if (sessionVar != null)
                    return sessionVar as AccountViewModel;

                return null;
            }
            set 
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }

        public static void LogOut()
        {
            HttpContext.Current.Session.Clear();
        }



        public static bool isAuthenticated
        {
            get 
            {
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                    return true;

                return false;
            }
        }
    }
}