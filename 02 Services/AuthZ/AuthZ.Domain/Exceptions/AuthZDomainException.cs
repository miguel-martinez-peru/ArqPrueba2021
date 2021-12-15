using System;

namespace AuthZ.Domain.Exceptions
{
    public class AuthZDomainException : Exception
    {
        public AuthZDomainException()
        { }

        public AuthZDomainException(string message)
            : base(message)
        { }

        public AuthZDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
