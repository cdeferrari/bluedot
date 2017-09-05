using Administracion.DomainModel.Enum;
using Administracion.Security;
using System.Web;
using System.Web.Mvc;

namespace Administracion.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {        
        private Roles allowRoles;     

        public CustomAuthorizeAttribute(Roles allowRoles)
        {
            this.allowRoles = allowRoles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            if (SessionPersister.isAuthenticated)
            {
                authorize = SessionPersister.Account.Role == allowRoles;
            }

            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!SessionPersister.isAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "AccessDenied", action = "Denied" }));
            }
            
        }
    } 
}