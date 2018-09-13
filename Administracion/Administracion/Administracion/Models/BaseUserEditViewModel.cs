using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Administracion.Models
{
    public class BaseUserEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ingrese un apellido")]
        [DisplayName("Apellido")]
        public string Surname { get; set; }
        public string DNI { get; set; }
        public string CUIT { get; set; }
        public string ProfilePicFileName { get; set; } = "avatar.jpg";
        public HttpPostedFileBase ProfilePic { get; set; }
        [DisplayName("Comentarios")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Ingrese una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Confirme contraseña")]
        [Required(ErrorMessage = "Vuelva a ingresar su contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas deben coincidir")]
        public string ConfirmPassword { get; set; }

        //Contact data
        [DisplayName("Telefono")]
        public string Telephone { get; set; }
        [DisplayName("Celular")]
        public string Cellphone { get; set; }
        [Required(ErrorMessage = "Ingrese un email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public void SetUser(DomainModel.User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.DNI = user.DNI;
            this.CUIT = user.CUIT;
            if(!string.IsNullOrEmpty(user.ProfilePic)) {
                this.ProfilePicFileName = user.ProfilePic;
            }
            this.Comments = user.Comments;
            if(user.ContactData != null)
            {
                this.Telephone = user.ContactData.Telephone;
                this.Cellphone = user.ContactData.Cellphone;
                this.Email = user.ContactData.Email;
            }
        }

        public void GetUser(ref DomainModel.User user)
        {
            user.Name = this.Name;
            user.Surname = this.Surname;
            user.DNI = this.DNI;
            user.CUIT = this.CUIT;
            user.Comments = this.Comments;
        }

        public void GetContactData(ref DomainModel.ContactData contactData)
        {
            contactData.Telephone = this.Telephone;
            contactData.Cellphone = this.Cellphone;
            contactData.Email = this.Email;
        }

    }
}