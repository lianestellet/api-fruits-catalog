using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.DTOs;
using API.Models.Request;
using BusinessLogic.Models.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{

    [ApiController]
    [Route("api/fruit")]
    public class FruitController(IFruitService fruitService) : ControllerBase
    {
        private readonly IFruitService _fruitService = fruitService;
        
        

        // GET: api/fruit
        [HttpGet]
        public async Task<ActionResult> FindAllFruits()
        {
            return Ok(await _fruitService.FindAllFruitsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindFruitById(long id)
        {
            return Ok(await _fruitService.FindFruitByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveFruit([FromBody] SaveFruitRequest saveFruitRequest)
        {
            if (saveFruitRequest == null)
            {
                return BadRequest();
            }

            var fruitResponse = await _fruitService.SaveFruitAsync(saveFruitRequest);
            return Ok(fruitResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFruit(long id, [FromBody] UpdateFruitRequest fruitDTO)
        {
            if (fruitDTO == null || id == 0)
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
