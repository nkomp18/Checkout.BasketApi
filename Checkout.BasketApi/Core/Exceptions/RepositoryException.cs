using System;

namespace Checkout.BasketApi.Core.Exceptions
{
    /// <summary>
    /// Equivalent to Sql/IO exception.
    /// </summary>
    public sealed class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message) { }
    }
}
