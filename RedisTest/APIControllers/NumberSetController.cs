using Microsoft.AspNetCore.Mvc;
using RedisTest.Models.Dto;
using RedisTest.Service.Interface;

namespace RedisTest.APIControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NumberSetController : ControllerBase
    {
        private readonly INumberService _numberService;

        public NumberSetController(INumberService numberService)
        {
            _numberService = numberService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _numberService.GetAll();

            return Ok(result);

        }

        [HttpPost]
        public IActionResult SetNumber([FromBody] NumberDto request)
        {
            var result = new NumberDto()
            {
                NumberSet = request.NumberSet,
            };

            _numberService.CreateNumber(result);
            return Ok();
        }
    }
}
