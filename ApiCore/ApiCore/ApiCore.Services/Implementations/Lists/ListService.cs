using ApiCore.Services.Contracts.Lists;
using System;
using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Attributes;
using ApiCore.Library.Exceptions;
using ApiCore.Library.Mensajes;
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Services.Implementations.Lists
{
    public class ListService : IListService
    {
        public IListRepository ListRepository { get; set; }
        public IConsortiumRepository ConsortiumRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }
        public ITaskResultRepository TaskResultRepository { get; set; }

        [Transaction]
        public List CreateList(ListRequest List)
        {
            var entityToInsert = new List() { };
            MergeList(entityToInsert, List);
            ListRepository.Insert(entityToInsert);
            return entityToInsert;
        }

        public List GetById(int ListId)
        {
            var List = ListRepository.GetById(ListId);
            if (List == null)
                throw new BadRequestException(ErrorMessages.ListaNoEncontrada);

            return List;
        }
        

        [Transaction]
        public List UpdateList(List originalList, ListRequest List)
        {            
            this.MergeList(originalList, List);
            ListRepository.Update(originalList);
            return originalList;

        }
        

        [Transaction]
        public void DeleteList(int ListId)
        {
            var List = ListRepository.GetById(ListId);
            ListRepository.Delete(List);
        }

        [Transaction]
        public IList<List> GetAll()
        {
            var Lists = ListRepository.GetAll();
            if (Lists == null)
                throw new BadRequestException(ErrorMessages.ListaNoEncontrada);

            var result = new List<List>();
            var enumerator = Lists.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }


        [Transaction]
        public IList<List> GetByConsortium(int id)
        {
            var Lists = ListRepository.GetByConsortium(id);
            if (Lists == null)
                throw new BadRequestException(ErrorMessages.ListaNoEncontrada);

            var result = new List<List>();
            var enumerator = Lists.GetEnumerator();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);

            }
            return result ;
        }





        

        private void MergeList(List originalList, ListRequest List)
        {
            originalList.Customer = List.Customer;           
            originalList.Consortium = this.ConsortiumRepository.GetById(List.ConsortiumId);
            originalList.OpenDate = List.OpenDate;
            originalList.Coments = List.Coments;
            originalList.Tasks = new List<TaskList>();            
            foreach (var task in List.Tasks)
            {
                if (task.Id == 0)
                {
                    originalList.Tasks.Add(
                    new TaskList()
                    {
                        List = originalList,
                        Id = task.Id,
                        Description = task.Description,
                        Result = TaskResultRepository.GetById(task.ResultId),
                        Coments = task.Coments,
                        Status = StatusRepository.GetById(task.StatusId)
                    });
                }
                else
                {
                    originalList.Tasks.Where(x => x.Id.Equals(task.Id)).FirstOrDefault().Description = task.Description;
                    originalList.Tasks.Where(x => x.Id.Equals(task.Id)).FirstOrDefault().Coments = task.Coments;
                    originalList.Tasks.Where(x => x.Id.Equals(task.Id)).FirstOrDefault().Result = TaskResultRepository.GetById(task.ResultId);
                    originalList.Tasks.Where(x => x.Id.Equals(task.Id)).FirstOrDefault().Status = StatusRepository.GetById(task.StatusId);
                }
                
            }

            
        }
        
    }
}
