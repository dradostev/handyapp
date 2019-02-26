using System;

namespace Handy.Domain.SharedContext.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message)
        {
            
        }
    }
}