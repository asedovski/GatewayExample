using AccessAllAgents.MicroService.Common.Constants;
using System;

namespace AccessAllAgents.MicroService.Common.Exceptions
{
    public class ControllerException : Exception
    {
        public ControllerException(int errorCode, string failureReason)
        {
            IsSuccessful = false;
            ErrorCode = errorCode;
            FailureReason = failureReason;
        }

        public ControllerException(ErrorCodes errorCode, string failureReason)
        {
            IsSuccessful = false;
            ErrorCode = (int) errorCode;
            FailureReason = failureReason;
        }

        public bool IsSuccessful { get; set; }
        public int ErrorCode { get; set; }
        public string FailureReason { get; set; }
    }
}