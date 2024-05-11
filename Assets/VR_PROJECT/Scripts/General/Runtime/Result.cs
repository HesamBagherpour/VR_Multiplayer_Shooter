namespace VR_PROJECT.General
{
    public class Result
    {
        public bool IsSuccess;
        public string ErrorCode;
        public string ErrorMessage;
    }
    
    public class Result<T> : Result
    {
        public T Data;
    }
}