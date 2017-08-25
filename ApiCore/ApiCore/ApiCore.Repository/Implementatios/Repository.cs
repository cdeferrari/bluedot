using ApiCore.Library.Exceptions;
using ApiCore.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Repository.Implementatios
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IDbContext Context { get; set; }
        public IDbSet<T> Entities
        {
            get { return this.Context.Set<T>(); }
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);

        }

        public virtual IQueryable<T> GetAll()
        {
            return Entities.AsQueryable();
        }

        public T GetById(int id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new NotFoundException();

            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}
