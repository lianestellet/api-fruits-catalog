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
            return Ok(await _fruitService.FindAllFruitsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FruitDTO>>> FindFruitById(long id)
        {
            return Ok(await _fruitService.FindFruitByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveFruit([FromBody] CreateFruitDTO createFruitDTO)
        {
            if (createFruitDTO == null)
            {
                return BadRequest();
            }

            var fruitDto = await _fruitService.SaveFruitAsync(createFruitDTO);
            return CreatedAtAction(nameof(FindFruitById), new { id = fruitDto.Id }, fruitDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(long id, [FromBody] FruitDTO fruitDTO)
        {
            if (fruitDTO == null || id != fruitDTO.Id)
            {
                return BadRequest();
            }

            var updatedFruit = await _fruitService.UpdateFruitAsync(fruitDTO);
            if (updatedFruit == null)
            {
                return NotFound();
            }

            return Ok(updatedFruit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(long id)
        {
            var existingFruit = await _fruitService.FindFruitByIdAsync(id);
            if (existingFruit == null)
            {
                return NotFound();
            }

            await _fruitService.DeleteFruitAsync(id);
            return NoContent();
        }
    }
}
