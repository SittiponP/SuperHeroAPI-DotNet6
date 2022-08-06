using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> Get()
        {
            return Ok(await _dataContext.Superheroes.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Superhero>> Get(int Id)
        {
            var hero = await _dataContext.Superheroes.FindAsync(Id);

            if(hero == null)
            {
                return BadRequest("Hero Not Found");
            }

            return Ok(hero);
        }


        [HttpPost]
        public async Task<ActionResult<List<Superhero>>> AddHero(Superhero hero)
        {
            _dataContext.Superheroes.Add(hero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Superheroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero request)
        {

            var dbHero = await _dataContext.Superheroes.FindAsync(request.Id);

            if(dbHero == null)
            {
                return BadRequest("Hero Not Found");
            }

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Superheroes.ToListAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Superhero>> Delete(int Id)
        {
            var dbHero = await _dataContext.Superheroes.FindAsync(Id);

            if (dbHero == null)
            {
                return BadRequest("Hero Not Found");
            }

            _dataContext.Superheroes.Remove(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Superheroes.ToListAsync());
        }
    }
}
