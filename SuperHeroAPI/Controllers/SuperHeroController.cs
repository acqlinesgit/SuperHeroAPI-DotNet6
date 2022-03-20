using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                 new SuperHero {
                Id = 1,
                 Name = "MINGHAN.ZHONG",
                FirstName = "ZHONG",
                LastName = "HAN",
                Place = "TOYUAN"
                },
                  new SuperHero {
                Id = 2,
                 Name = "KOBE BRYANT",
                FirstName = "KOBE",
                LastName = "BRYANT",
                Place = "USA"
                }
            };
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<SuperHero>> Get()
        {
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }
        [HttpGet("GETID")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("NOT FOUND");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> addHero(SuperHero hero)
        {
            try
            {
                if (hero.Id.ToString().Trim() == string.Empty || hero.Id <= 0)
                {
                    return BadRequest("id 不能小於0");
                }
                if (hero.Name.Trim() == String.Empty ||
                    hero.FirstName.Trim() == String.Empty ||
                    hero.LastName.Trim() == String.Empty ||
                    hero.Place.Trim() == String.Empty)
                {
                    return BadRequest("所有值為必填, 請確認。");
                }
                _context.SuperHeroes.Add(hero);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException);
            }
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero requset)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(requset.Id);
            if (dbHero == null)
            {
                return BadRequest("NOT FOUND");
            }
            dbHero.Name = requset.Name;
            dbHero.FirstName = requset.FirstName;
            dbHero.LastName = requset.LastName;
            dbHero.Place = requset.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToArrayAsync());
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("NOT FOUND DELETE");
            }
            else
            {
                _context.SuperHeroes.Remove(dbHero);
                await _context.SaveChangesAsync();
                return Ok(await _context.SuperHeroes.ToArrayAsync());
            }
        }
    }
}
