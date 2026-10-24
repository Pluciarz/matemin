namespace MatemIn.Backend.Models;

public class Moderator : ZalogowanyUczen
{
    public void ZatwierdzZadanie(Zadanie zadanie)
    {
        zadanie.ZmienStatus(StatusZadania.ZATWIERDZONE);
    }

    public void OdrzucZadanie(Zadanie zadanie)
    {
        zadanie.ZmienStatus(StatusZadania.ODRZUCONE);
    }

    public void ZablokujUzytkownika(ZalogowanyUczen uzytkownik)
    {
        uzytkownik.CzyZablokowany = true;
    }
}
