using MatemIn.Backend.Data;
using MatemIn.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatemIn.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZadaniaController : ControllerBase
{
    private readonly MatemInDbContext _dbContext;

    public ZadaniaController(MatemInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetZatwierdzoneZadania([FromQuery] int? dzialId = null)
    {
        var query = _dbContext.Zadania
            .Include(z => z.Dzial)
            .Where(z => z.Status == StatusZadania.ZATWIERDZONE);

        if (dzialId.HasValue)
        {
            query = query.Where(z => z.DzialId == dzialId.Value);
        }

        var zadania = await query
            .OrderByDescending(z => z.DataDodania)
            .Select(z => new
            {
                z.Id,
                z.Tresc,
                Status = z.Status.ToString(),
                z.DataDodania,
                z.DzialId,
                DzialNazwa = z.Dzial != null ? z.Dzial.Nazwa : null
            })
            .ToListAsync();

        return Ok(zadania);
    }
}
