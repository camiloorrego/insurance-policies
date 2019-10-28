using System;

namespace Insurance.Policies.Domain.Exceptions
{
    public class PolicyInvalidException : Exception
    {
        public PolicyInvalidException(string message) : base(message)
        {
        }
    }
}
