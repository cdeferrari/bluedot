using Administracion.DomainModel;
using Administracion.Dto.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Lists
{
    public interface IChecklistService
    {
        IList<List> GetAll();
        List GetList(int ListId);
        bool CreateList(ListRequest List);
        bool UpdateList(ListRequest List);
        bool DeleteList(int ListId);

        IList<Item> GetItems();

    }
}
