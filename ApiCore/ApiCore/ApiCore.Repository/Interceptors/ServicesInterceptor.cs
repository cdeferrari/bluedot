using ApiCore.Repository.Attributes;
using ApiCore.Repository.Contracts;
using Castle.DynamicProxy;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace ApiCore.Repository.Interceptors
{
    public class ServicesInterceptor : IInterceptor
    {
        public IDbContext _context
        {
            get; set;
        }
        private DbContextTransaction _transaction;

        public void Intercept(IInvocation invocation)
        {
            var isNestedTransaction = IsNestedTransaction(invocation.MethodInvocationTarget);
            var isTransactional = IsTransactional(invocation.MethodInvocationTarget);

            if (_transaction == null && isTransactional)
            {
                _transaction = _context.Database.BeginTransaction();
            }

            try
            {
                invocation.Proceed();
                if (_transaction != null && !isNestedTransaction)
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                    _transaction = null;
                }
            }
            catch (Exception ex)
            {   
                if (_transaction != null)
                {
                    if (_context.Database.CurrentTransaction != null)
                        _transaction.Rollback();
                    _transaction.Dispose();
                    _context.Dispose();
                    _transaction = null;
                }
                throw;
            }
        }

        private static bool IsTransactional(MethodInfo methodInfo)
        {
            var result = methodInfo.GetCustomAttributes(false).OfType<TransactionAttribute>().Any();

            if (!result && methodInfo.DeclaringType != null)
                result = methodInfo.DeclaringType.GetCustomAttributes(false).OfType<TransactionAttribute>().Any();

            return result;
        }

        private static bool IsNestedTransaction(MethodInfo methodInfo)
        {
            var result = methodInfo.GetCustomAttributes(false).OfType<NestedTransactionAttribute>().Any();

            if (!result && methodInfo.DeclaringType != null)
                result = methodInfo.DeclaringType.GetCustomAttributes(false).OfType<NestedTransactionAttribute>().Any();

            return result;
        }

        private static IsolationLevel GetIsolationLevel(MethodInfo methodInfo)
        {
            foreach (var prop in methodInfo.GetCustomAttributes(false).OfType<TransactionAttribute>())
            {
                return prop.IsolationLevel;
            }

            if (methodInfo.DeclaringType != null)
                foreach (var prop in methodInfo.DeclaringType.GetCustomAttributes(false).OfType<TransactionAttribute>())
                {
                    return prop.IsolationLevel;
                }

            return IsolationLevel.ReadCommitted;
        }
    }
}
