using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using EFCoreMovies.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ActorsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<ActorDto>> GetActors(int page = 1, int recordsToTake = 2)
    {
        return await _context.Actors.AsNoTracking()
            .OrderBy(g => g.Name)
            .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
            .Paginate(page, recordsToTake)
            .ToListAsync();
    }
}
