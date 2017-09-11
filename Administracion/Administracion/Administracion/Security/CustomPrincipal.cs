using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Administracion.Models;
using Administracion.DomainModel;

namespace Administracion.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private Account account;

        public CustomPrincipal(Account account)
        {
            this.account = account;
            this.Identity = new GenericIdentity(account.Name);
        }

        public IIdentity Identity
        {
            get;
            set;
        }


        public bool IsInRole(string role)
        {
            return true;
        }
    }
}