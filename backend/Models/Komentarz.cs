namespace MatemIn.Backend.Models;

public class Komentarz
{
    public int Id { get; set; }
    public string Tresc { get; set; } = string.Empty;
    public DateTime DataDodania { get; set; } = DateTime.UtcNow;

    public int RozwiazanieId { get; set; }
    public Rozwiazanie Rozwiazanie { get; set; } = null!;

    public int AutorId { get; set; }
    public ZalogowanyUczen Autor { get; set; } = null!;
}
