using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CinemasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CinemasController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<CinemaDto>> GetCinemas()
    {
        return await _context.Cinemas.ProjectTo<CinemaDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    [HttpGet("closeToMe")]
    public async Task<ActionResult> GetCinemasCloseToMe(double latitude, double longitude)
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        var myLocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

        var cinemas = await _context.Cinemas
            .OrderBy(c => c.Location.Distance(myLocation))
            .Select(c => new
            {
                c.Name,
                Distance = Math.Round(c.Location.Distance(myLocation), 6)
            }).ToListAsync();

        return Ok(cinemas);
    }
}
