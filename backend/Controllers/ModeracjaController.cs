using MateMin.Backend.Data;
using MateMin.Backend.Dtos;
using MateMin.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MateMin.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeracjaController : ControllerBase
{
    private readonly MateMinDbContext _db;

    public ModeracjaController(MateMinDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Kolejka moderacji: zadania oczekujÄ…ce na decyzjÄ™, od najstarszych.
    /// </summary>
    [HttpGet("zadania/oczekujace")]
    public async Task<ActionResult<List<ZadanieDoModeracjiDto>>> PobierzOczekujace()
    {
        var zadania = await _db.Zadania
            .AsNoTracking()
            .Include(z => z.Autor)
            .Include(z => z.Dzial)
            .Where(z => z.Status == StatusZadania.OCZEKUJACE)
            .OrderBy(z => z.DataDodania)
            .ToListAsync();

        var wynik = zadania
            .Select(z => new ZadanieDoModeracjiDto
            {
                Id = z.Id,
                Tresc = z.Tresc,
                DataDodania = z.DataDodania,
                AutorNazwa = z.Autor.NazwaUzytkownika,
                DzialNazwa = z.Dzial?.Nazwa ?? "Brak dziaĹ‚u",
                Status = z.Status
            })
            .ToList();

        return Ok(wynik);
    }

    /// <summary>
    /// Zatwierdza zadanie.
    /// </summary>
    [HttpPatch("zadania/{id:int}/zatwierdz")]
    public async Task<IActionResult> Zatwierdz(int id)
    {
        var zadanie = await _db.Zadania.FindAsync(id);
        if (zadanie is null)
        {
            return NotFound();
        }

        zadanie.ZmienStatus(StatusZadania.ZATWIERDZONE);
        await _db.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Odrzuca zadanie.
    /// </summary>
    [HttpPatch("zadania/{id:int}/odrzuc")]
    public async Task<IActionResult> Odrzuc(int id)
    {
        var zadanie = await _db.Zadania.FindAsync(id);
        if (zadanie is null)
        {
            return NotFound();
        }

        zadanie.ZmienStatus(StatusZadania.ODRZUCONE);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
