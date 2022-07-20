using EFCoreMovies.Entities;
using EFCoreMovies.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _context;

    public GenresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Genre>> GetGenres(int page = 1, int recordsToTake = 2)
    {
        return await _context.Genres.AsNoTracking()
            .OrderBy(g => g.Name)
            .Paginate(page, recordsToTake)
            .ToListAsync();
    }

    [HttpGet("first")]
    public async Task<ActionResult<Genre>> GetFirstGenre()
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name.Contains('z'));

        if (genre is null) return NotFound();

        return genre;
    }

    [HttpGet("filter")]
    public async Task<List<Genre>> GetGenresFiltered(string name)
    {
        return await _context.Genres.Where(g => g.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();
    }
}
