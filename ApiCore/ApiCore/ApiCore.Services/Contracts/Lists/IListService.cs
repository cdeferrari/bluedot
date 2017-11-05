using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.Lists
{
    public interface IListService
    {
        List CreateList(ListRequest List);
        List GetById(int ListId);
        IList<List> GetAll();
        IList<List> GetByConsortium(id);
        List UpdateList(List originalList, ListRequest List);
        void DeleteList(int ListId);
    }
}
