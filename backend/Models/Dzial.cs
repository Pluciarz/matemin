namespace MatemIn.Backend.Models;

public class Dzial
{
    public int Id { get; set; }
    public string Nazwa { get; set; } = string.Empty;

    public int? RodzicId { get; set; }
    public Dzial? Rodzic { get; set; }

    public ICollection<Dzial> Poddzialy { get; set; } = new List<Dzial>();
    public ICollection<Zadanie> Zadania { get; set; } = new List<Zadanie>();

    public void DodajPoddzial(Dzial poddzial)
    {
        poddzial.Rodzic = this;
        poddzial.RodzicId = Id;
        Poddzialy.Add(poddzial);
    }
}
