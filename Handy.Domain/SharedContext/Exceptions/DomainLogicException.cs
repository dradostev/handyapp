using System;

namespace Handy.Domain.SharedContext.Exceptions
{
    public class DomainLogicException : Exception
    {
        public DomainLogicException(string message) : base(message)
        {
            
        }
    }
}