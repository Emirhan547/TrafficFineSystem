namespace TrafficFineSystem.Services.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }

        private ServiceResult()
        {
        }

        public static ServiceResult Success()
        {
            return new ServiceResult
            {
                IsSuccess = true
            };
        }

        public static ServiceResult Failure(string errorMessage)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
