using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.Service;
using WebAPI.Models.Response;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("wfapi/[controller]")]
    public class WeatherForecastController(IMGMService mgmService) : ControllerBase
    {
        private readonly IMGMService _mgmService = mgmService;

        [HttpGet("GetProvinces")]
        public async Task<ActionResult<IEnumerable<RProvince>>> GetProvinces(string? filter)
        {
            var result = await _mgmService.GetProvinces(filter);
            if (!result.Result)
            {
                return BadRequest();
            }
            return Ok(result.SuccessData);
        }

        [HttpGet("GetDistricts")]
        public async Task<ActionResult<IEnumerable<RDistrict>>> GetDistricts([FromQuery] string province, string? filter)
        {
            var result = await _mgmService.GetDistricts(province, filter);
            if (!result.Result)
            {
                return BadRequest();
            }
            return Ok(result.SuccessData);
        }
    }
}
