namespace WebAPI.Models.Result
{
    public interface IDataResult<T> : IResult
    {

        T SuccessData { get; set; }
    }
}
