namespace MatemIn.Backend.Models;

public class Zadanie
{
    public int Id { get; set; }
    public string Tresc { get; set; } = string.Empty;
    public StatusZadania Status { get; set; } = StatusZadania.OCZEKUJACE;
    public DateTime DataDodania { get; set; } = DateTime.UtcNow;

    public int? DzialId { get; set; }
    public Dzial? Dzial { get; set; }

    public int AutorId { get; set; }
    public ZalogowanyUczen Autor { get; set; } = null!;

    public ICollection<Rozwiazanie> Rozwiazania { get; set; } = new List<Rozwiazanie>();

    public void ZmienStatus(StatusZadania nowyStatus)
    {
        Status = nowyStatus;
    }
}
