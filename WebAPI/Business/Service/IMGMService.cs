using WebAPI.Models.Response;
using WebAPI.Models.Result;

namespace WebAPI.Business.Service
{
    public interface IMGMService
    {
        Task<IDataResult<List<RProvince>>> GetProvinces(string? filter);
    }
}
