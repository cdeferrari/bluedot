using Administracion.DomainModel;
using Administracion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.SpendItemsService
{
    public interface ISpendItemsService
    {
        IList<SpendItem> GetAll();        
    }
}
