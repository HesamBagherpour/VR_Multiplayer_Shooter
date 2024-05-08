namespace VR_PROJECT.General
{
    public class Result<T>
    {
        public T Data;
        public bool IsSuccess;
        public string ErrorCode;
        public string ErrorMessage;
    }
}