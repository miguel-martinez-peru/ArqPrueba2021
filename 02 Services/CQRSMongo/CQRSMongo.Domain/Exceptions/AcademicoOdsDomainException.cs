using System;

namespace Sunedu.Siu.Service.Institucional.Domain.Exceptions
{
    public class AcademicoOdsDomainException : Exception
    {
        public AcademicoOdsDomainException() 
        { }

        public AcademicoOdsDomainException(string message): base(message)
        { }
        public AcademicoOdsDomainException(string message, Exception innerException): base(message, innerException)
        { }
    }
}
