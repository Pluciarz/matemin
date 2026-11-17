using MatemIn.Backend.Data;
using MatemIn.Backend.Dtos;
using MatemIn.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatemIn.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeracjaController : ControllerBase
{
    private readonly MatemInDbContext _db;

    public ModeracjaController(MatemInDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Kolejka moderacji: zadania oczekujące na decyzję, od najstarszych.
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
                DzialNazwa = z.Dzial?.Nazwa ?? "Brak działu",
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
