using System.ComponentModel.DataAnnotations;

namespace MatemIn.Backend.Models;

public class Ocena
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Wartosc { get; set; }

    public DateTime DataDodania { get; set; } = DateTime.UtcNow;

    public int RozwiazanieId { get; set; }
    public Rozwiazanie Rozwiazanie { get; set; } = null!;

    public int AutorId { get; set; }
    public ZalogowanyUczen Autor { get; set; } = null!;
}
