namespace WebAPI.Models.Result
{
    public class SuccessDataResult<T> : SuccessResult, IDataResult<T>
    {
        public SuccessDataResult(T data)
        {
            SuccessData = data;
            Result = true;
        }
        public T SuccessData { get; set; }
    }
}
