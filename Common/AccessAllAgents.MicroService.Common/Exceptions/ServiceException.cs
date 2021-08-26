using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Extensions;
using System;

namespace AccessAllAgents.MicroService.Common.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(ErrorCodes code)
            : base(code.ToFailureReason())
        {
            ErrorCode = (int) code;
        }

        public ServiceException(ErrorCodes code, string message)
            : base(message)
        {
            ErrorCode = (int) code;
        }

        public ServiceException(ErrorCodes code, string message, Exception cause)
            : base(message, cause)
        {
            ErrorCode = (int) code;
        }

        public int ErrorCode { get; }
    }
}