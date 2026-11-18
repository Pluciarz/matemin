using MatemIn.Backend.Models;
using Xunit;

namespace MatemIn.Tests;

public class DomainModelsTests
{
    [Fact]
    public void ObliczSredniaOcene_GdyBrakOcen_ZwracaZero()
    {
        var rozwiazanie = new Rozwiazanie
        {
            Id = 1,
            Tresc = "Rozwi¹zanie równania liniowego: x = 5"
        };

        var srednia = rozwiazanie.ObliczSredniaOcene();

        Assert.Equal(0.0, srednia);
    }

    [Fact]
    public void ObliczSredniaOcene_GdyDodanoOceny_ZwracaPoprawnaSrednia()
    {
        var rozwiazanie = new Rozwiazanie
        {
            Id = 1,
            Tresc = "Dowód twierdzenia Pitagorasa",
            Oceny = new List<Ocena>
            {
                new() { Id = 1, Wartosc = 4 },
                new() { Id = 2, Wartosc = 5 },
                new() { Id = 3, Wartosc = 3 }
            }
        };

        var srednia = rozwiazanie.ObliczSredniaOcene();

        Assert.Equal(4.0, srednia, precision: 5);
    }

    [Fact]
    public void ZatwierdzZadanie_ZmieniaStatusNaZatwierdzone()
    {
        var moderator = new Moderator { Id = 1 };
        var zadanie = new Zadanie
        {
            Id = 10,
            Tresc = "Oblicz ca³kê oznaczon¹ z funkcji f(x) = x^2 w przedziale [0, 1]."
        };

        moderator.ZatwierdzZadanie(zadanie);

        Assert.Equal(StatusZadania.ZATWIERDZONE, zadanie.Status);
    }

    [Fact]
    public void OdrzucZadanie_ZmieniaStatusNaOdrzucone()
    {
        var moderator = new Moderator { Id = 1 };
        var zadanie = new Zadanie
        {
            Id = 11,
            Tresc = "Niekompletna treœæ zadania"
        };

        moderator.OdrzucZadanie(zadanie);

        Assert.Equal(StatusZadania.ODRZUCONE, zadanie.Status);
    }

    [Fact]
    public void ZablokujUzytkownika_UstawiaFlageCzyZablokowanyNaTrue()
    {
        var moderator = new Moderator { Id = 1 };
        var uczen = new ZalogowanyUczen
        {
            Id = 2,
            CzyZablokowany = false
        };

        moderator.ZablokujUzytkownika(uczen);

        Assert.True(uczen.CzyZablokowany);
    }

    [Fact]
    public void DodajPoddzial_UstawiaRodzicaIDodajeDoKolekcjiPoddzialow()
    {
        var dzialGlowny = new Dzial
        {
            Id = 1,
            Nazwa = "Algebra",
            Poddzialy = new List<Dzial>()
        };
        var poddzial = new Dzial
        {
            Id = 2,
            Nazwa = "Wielomiany",
            Poddzialy = new List<Dzial>()
        };

        dzialGlowny.DodajPoddzial(poddzial);

        Assert.Contains(poddzial, dzialGlowny.Poddzialy);
        Assert.Same(dzialGlowny, poddzial.Rodzic);
        Assert.Equal(dzialGlowny.Id, poddzial.RodzicId);
    }
}