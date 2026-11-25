
using MateMin.Backend.Data;
using MateMin.Backend.Dtos;
using MateMin.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MateMin.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DzialyController : ControllerBase
{
    private readonly MateMinDbContext _dbContext;

    public DzialyController(MateMinDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<DzialDto>>> GetDzialy()
    {
        if (!await _dbContext.Dzialy.AnyAsync())
        {
            var funkcje = new Dzial { Nazwa = "Funkcje" };
            funkcje.DodajPoddzial(new Dzial { Nazwa = "Funkcja liniowa" });
            funkcje.DodajPoddzial(new Dzial { Nazwa = "Funkcja kwadratowa" });

            var ciagi = new Dzial { Nazwa = "CiÄ…gi" };
            ciagi.DodajPoddzial(new Dzial { Nazwa = "CiÄ…g arytmetyczny" });
            ciagi.DodajPoddzial(new Dzial { Nazwa = "CiÄ…g geometryczny" });

            var trygonometria = new Dzial { Nazwa = "Trygonometria" };
            trygonometria.DodajPoddzial(new Dzial { Nazwa = "Funkcje trygonometryczne" });

            var geometria = new Dzial { Nazwa = "Geometria" };
            geometria.DodajPoddzial(new Dzial { Nazwa = "Planimetria" });
            geometria.DodajPoddzial(new Dzial { Nazwa = "Stereometria" });

            _dbContext.Dzialy.AddRange(
                funkcje,
                ciagi,
                trygonometria,
                geometria
            );

            await _dbContext.SaveChangesAsync();
        }

        var dzialy = await _dbContext.Dzialy
            .Where(d => d.RodzicId == null)
            .Include(d => d.Poddzialy)
            .OrderBy(d => d.Nazwa)
            .ToListAsync();

        var wynik = dzialy.Select(d => new DzialDto(
            d.Id,
            d.Nazwa,
            d.Poddzialy
                .OrderBy(p => p.Nazwa)
                .Select(p => new PoddzialDto(
                    p.Id,
                    p.Nazwa,
                    p.RodzicId
                ))
                .ToList()
        )).ToList();

        return Ok(wynik);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DzialDto>> GetDzial(int id)
    {
        var dzial = await _dbContext.Dzialy
            .Include(d => d.Poddzialy)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dzial == null)
        {
            return NotFound();
        }

        var wynik = new DzialDto(
            dzial.Id,
            dzial.Nazwa,
            dzial.Poddzialy
                .OrderBy(p => p.Nazwa)
                .Select(p => new PoddzialDto(
                    p.Id,
                    p.Nazwa,
                    p.RodzicId
                ))
                .ToList()
        );

        return Ok(wynik);
    }
}
