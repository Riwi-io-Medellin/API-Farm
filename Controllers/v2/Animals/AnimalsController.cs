using API_Farm.Data;
using API_Farm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Farm.Controllers.v2.Animals;

[ApiController]
[Route("api/v2/[controller]")]
[ApiExplorerSettings(GroupName = "v2")]
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
        var animals = await Context.Animals.Include(p => p.AnimalType).ToListAsync();

        if (animals.Any() == false)
        {
            return NoContent();
        }
        return Ok(animals);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var animal = await Context.Animals.Include(p => p.AnimalType).FirstOrDefaultAsync(p => p.Id == id);
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
                            ).Include(p => p.AnimalType).ToListAsync();
        if (animals.Any() == false)
        {
            return NoContent();
        }
        return Ok(animals);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Animal newanimal)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        Context.Animals.Add(newanimal);
        await Context.SaveChangesAsync();
        return Ok("created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Animal updatedanimal)
    {
        var animal = checkExistence(id);
        if (animal == false)
        {
            return NoContent();
        }
        updatedanimal.Id = id;
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        Context.Entry(updatedanimal).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return Ok("updated");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var animal = checkExistence(id);
        if (animal == false)
        {
            return NoContent();
        }
        Context.Animals.Remove(await Context.Animals.FindAsync(id));
        await Context.SaveChangesAsync();
        return Ok("deleted");
    }


    private bool checkExistence(int id)
    {
        return Context.Animals.Any(p => p.Id == id);
    }


}
