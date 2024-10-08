using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
    {
        var heroes = await _context.SuperHeroes.ToListAsync();

        return Ok(heroes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero is null)
        {
            return NotFound("Hero not found");
        }

        return Ok(hero);
    }

    // probably want to make a DTO for this
    [HttpPost]
    public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync();

        return Ok(hero);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero hero)
    {
        var existingHero = await _context.SuperHeroes.FindAsync(id);
        if (existingHero is null)
        {
            return NotFound("Hero not found");
        }

        existingHero.Name = hero.Name;
        existingHero.FirstName = hero.FirstName;
        existingHero.LastName = hero.LastName;
        existingHero.Place = hero.Place;

        await _context.SaveChangesAsync();

        return Ok(existingHero);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SuperHero>> DeleteHero(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero is null)
        {
            return NotFound("Hero not found");
        }

        _context.SuperHeroes.Remove(hero);
        await _context.SaveChangesAsync();

        return Ok(hero);
    }
}
