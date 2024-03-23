using System;

namespace Domain.Exceptions
{
    public class ApiNotFoundException : Exception
    {
        public ApiNotFoundException(string message) : base(message)
        {
        }
    }
}