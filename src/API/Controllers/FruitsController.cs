using Entities.DTOs;
using Entities.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> SaveFruit([FromBody] FruitDTO fruitDTO)
        {
            if (fruitDTO == null)
            {
                return BadRequest();
            }

            await _fruitService.SaveFruit(fruitDTO);
            return CreatedAtAction(nameof(FindFruitById), new { id = fruitDTO.Id }, fruitDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(long id, [FromBody] FruitDTO fruitDTO)
        {
            if (fruitDTO == null || id != fruitDTO.Id)
            {
                return BadRequest();
            }

            var updatedFruit = await _fruitService.UpdateFruit(fruitDTO);
            if (updatedFruit == null)
            {
                return NotFound();
            }

            return Ok(updatedFruit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(long id)
        {
            var existingFruit = await _fruitService.FindFruitById(id);
            if (existingFruit == null)
            {
                return NotFound();
            }

            await _fruitService.DeleteFruit(id);
            return NoContent();
        }
    }
}
