using FruitApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using FruitApplication.BusinessLogic;

namespace FruitApplication.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FruitsController : ControllerBase
    {
        private readonly IFruitService _fruitService;
        public FruitsController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }

        // GET: api/Fruits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindAll()
        {
            return Ok(await _fruitService.FindAllFruits());
        }
    }
}
