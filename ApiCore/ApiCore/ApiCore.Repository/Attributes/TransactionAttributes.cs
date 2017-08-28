using System;
using System.Data;

namespace ApiCore.Repository.Attributes
{
    /// <summary>
    /// Attributo utilizado para describir el comportamiento transaccional de un metodo o clase.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class TransactionAttribute : Attribute
    {
        /// <summary> 
        /// Obtiene o setea el valor indicando si la transacción es de solo lectura.</summary> 
        /// <remarks>Por defecto es false.</remarks>
        public bool ReadOnly { get; set; }

        private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

        /// <summary> 
        ///   Obtengo el isolation level para la transacción.</summary> 
        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        public TransactionAttribute()
        {
        }

        public TransactionAttribute(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }
    }

    /// <summary>
    /// Attributo utilizado para describir el comportamiento transaccional de un metodo o clase.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NestedTransactionAttribute : Attribute
    {
        /// <summary> 
        /// Obtiene o setea el valor indicando si la transacción es de solo lectura.</summary> 
        /// <remarks>Por defecto es false.</remarks>
        public bool ReadOnly { get; set; }

        private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

        /// <summary> 
        ///   Obtengo el isolation level para la transacción.</summary> 
        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        public NestedTransactionAttribute()
        {
        }

        public NestedTransactionAttribute(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }
    }
}
