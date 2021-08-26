using AccessAllAgents.MicroService.Common.Constants;

namespace AccessAllAgents.MicroService.Template.ViewModels
{
    public class GenericResponseViewModel
    {
        public bool IsSuccessful { get; set; }
        public int ErrorCode { get; set; }
        public string FailureReason { get; set; }

        public static GenericResponseViewModel Success => new GenericResponseViewModel
        {
            IsSuccessful = true,
            ErrorCode = (int) ErrorCodes.None,
            FailureReason = null
        };
    }
}