using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindAllFruits()
        {
            return Ok(await _fruitService.FindAllFruits());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindFruitById(long id)
        {
            return Ok(await _fruitService.FindFruitById(id));
        }
    }
}
