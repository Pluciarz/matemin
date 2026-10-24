namespace MatemIn.Backend.Models;

public class ZalogowanyUczen : Uzytkownik
{
    public string Email { get; set; } = string.Empty;
    public string HasloHash { get; set; } = string.Empty;
    public string NazwaUzytkownika { get; set; } = string.Empty;
    public DateOnly DataUrodzenia { get; set; }
    public DateTime DataRejestracji { get; set; } = DateTime.UtcNow;
    public bool CzyZablokowany { get; set; } = false;

    public ICollection<Zadanie> Zadania { get; set; } = new List<Zadanie>();
    public ICollection<Rozwiazanie> Rozwiazania { get; set; } = new List<Rozwiazanie>();
    public ICollection<Komentarz> Komentarze { get; set; } = new List<Komentarz>();
    public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
}
