namespace TraceIQ.Expeditor.Models
{
    public class OperationResult
    {
        public string ErrorMessage { get; set; }

        public OperationStatus Result { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult
            {
                Result = OperationStatus.Success
            };
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult
            {
                Result = OperationStatus.Failed,
                ErrorMessage = message
            };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Value { get; set; }

        public static OperationResult<T> Success(T value, string message = null)
        {
            return new OperationResult<T>
            {
                Value = value,
                Result = OperationStatus.Success,
                ErrorMessage = message
            };
        }

        public static OperationResult<T> Fail(string errorMessage)
        {
            return new OperationResult<T>
            {
                Value = default,
                ErrorMessage = errorMessage,
                Result = OperationStatus.Failed
            };
        }
    }

    public enum OperationStatus
    {
        Success,
        Failed,
    }
}
