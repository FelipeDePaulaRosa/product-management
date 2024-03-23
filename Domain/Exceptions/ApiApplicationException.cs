using System;

namespace Domain.Exceptions
{
    public class ApiApplicationException : ApplicationException
    {
        public ApiApplicationException(string message) : base(message)
        {
        }
    }
}