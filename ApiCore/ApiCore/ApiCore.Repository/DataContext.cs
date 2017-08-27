using ApiCore.DomainModel;
using ApiCore.Repository.Contracts;
using ApiCore.Repository.Interceptors;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using System.Web.Configuration;

namespace ApiCore.Repository
{
    public class DataContext : DbContext, IDbContext
    {
        private int _timeout;
        private int Timeout
        {
            get
            {
                if (_timeout == 0)
                {
                    _timeout = int.Parse(WebConfigurationManager.AppSettings["db.timeout"]);
                }
                return _timeout;
            }
        }


        public DataContext():base("name=connectionString")
        {
            //DbInterception.Add(new NoExpandInterceptor());
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            this.Database.CommandTimeout = Timeout;

            base.OnModelCreating(modelBuilder);
        }

        IDbSet<T> IDbContext.Set<T>()
        {
            return base.Set<T>();
        }
    }
}