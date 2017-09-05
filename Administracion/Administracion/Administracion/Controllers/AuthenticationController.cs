using System.Web.Mvc;
using Administracion.Services.Contracts.Autentication;
using Administracion.Models;
using Administracion.Security;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Administracion.DomainModel.Enum;

namespace Administracion.Controllers
{
    public class AuthenticationController : Controller
    {

        public virtual IAuthentication autenticationService { get; set; }
        

        [AllowAnonymous]
        // GET: Autentication
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult TestUpload()
        {
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel account)
        {

            if (ModelState.IsValid)
            {
                var accountAutenticated = this.autenticationService.Login(account.Email, this.MD5Hash(account.Password));

                if (accountAutenticated != null)
                {

                    SessionPersister.Account = Mapper.Map<AccountViewModel>(accountAutenticated);
                    if (SessionPersister.Account.Role == Roles.Root)
                        return RedirectToAction("Index", "Home");
                    if (SessionPersister.Account.Role == Roles.Client)
                        return RedirectToAction("Index", "Client");
            
                }
                else
                {
                    account.HasErrors = true;
                    account.MessageError = "El usuario/password ingresado es Invalido, por favor vuelva a internarlo";
                    return View(account);
                }
            }

            return View();
        }




        [IsLoggedAuthorizeAttribute]
        public ActionResult LogOut()
        {
            SessionPersister.LogOut();
            return RedirectToAction("Login");
        }


        private string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
