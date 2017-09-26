using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Administrations
{
    public interface IAdministrationService
    {
        Administration CreateAdministration(AdministrationRequest Administration);
        Administration GetById(int AdministrationId);
        Administration UpdateAdministration(Administration originalAdministration, AdministrationRequest Administration);
        void DeleteAdministration(int AdministrationId);
        IList<Administration> GetAll();
    }
}
