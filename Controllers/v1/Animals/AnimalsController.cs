using API_Farm.Data;
using API_Farm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Farm.Controllers.v1.Animals
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AnimalsController : ControllerBase
    {
        private readonly ApplicationDbContext Context;

        public AnimalsController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieves all animals",
            Description = "Gets a list of all animals in the database."
        )]
        [SwaggerResponse(200, "Returns a list of animals.", typeof(IEnumerable<Animal>))]
        [SwaggerResponse(204, "There are no registered animals.")]
        [SwaggerResponse(500, "An internal server error occurred.")]
        public async Task<IActionResult> GetAll()
        {
            var animals = await Context.Animals.ToListAsync();

            if (animals.Any() == false)
            {
                return NoContent();
            }
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var animal = await Context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NoContent();
            }
            return Ok(animal);
        }


        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> SearchByKeyword([FromRoute] string keyword)
        {
            var animals = await Context.Animals
                                .Where
                                (p => p.Name.Contains(keyword) ||
                                p.Description.Contains(keyword) ||
                                p.WeightKg.ToString().Contains(keyword)
                                ).ToListAsync();
            if (animals.Any() == false)
            {
                return NoContent();
            }
            return Ok(animals);
        }



    }
}