using System;

namespace Insurance.Policies.Domain.Exceptions
{
    public class UserUnAuthException : Exception
    {
        public UserUnAuthException(string message) : base(message)
        {
        }
    }
}
