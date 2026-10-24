namespace MatemIn.Backend.Models;

public class Rozwiazanie
{
    public int Id { get; set; }
    public string Tresc { get; set; } = string.Empty;
    public DateTime DataDodania { get; set; } = DateTime.UtcNow;

    public int ZadanieId { get; set; }
    public Zadanie Zadanie { get; set; } = null!;

    public int AutorId { get; set; }
    public ZalogowanyUczen Autor { get; set; } = null!;

    public ICollection<Komentarz> Komentarze { get; set; } = new List<Komentarz>();
    public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();

    public double ObliczSredniaOcene()
    {
        if (Oceny.Count == 0)
            return 0.0;

        return Oceny.Average(o => o.Wartosc);
    }
}
