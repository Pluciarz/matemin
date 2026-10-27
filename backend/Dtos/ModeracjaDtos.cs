using System.ComponentModel.DataAnnotations;
using MatemIn.Backend.Models;

namespace MatemIn.Backend.Dtos;

/// <summary>
/// Zadanie wyświetlane na liście moderatora.
/// </summary>
public record ZadanieDoModeracjiDto
{
    public required int Id { get; init; }
    public required string Tresc { get; init; }
    public required DateTime DataDodania { get; init; }
    public required string AutorNazwa { get; init; }
    public required string DzialNazwa { get; init; }
    public required StatusZadania Status { get; init; }
}

/// <summary>
/// Żądanie zmiany statusu zadania przez moderatora.
/// </summary>
public record ZmianaStatusuZadaniaDto
{
    public required StatusZadania NowyStatus { get; init; }

    [MaxLength(1000)]
    public string? PowodOdrzucenia { get; init; }
}

/// <summary>
/// Użytkownik wyświetlany na liście moderatora.
/// </summary>
public record UzytkownikModeracjaDto
{
    public required int Id { get; init; }
    public required string NazwaUzytkownika { get; init; }
    public required string Email { get; init; }
    public required DateTime DataRejestracji { get; init; }
    public required bool CzyZablokowany { get; init; }
}

/// <summary>
/// Żądanie zablokowania lub odblokowania użytkownika.
/// </summary>
public record ZmianaBlokadyUzytkownikaDto
{
    public required bool CzyZablokowany { get; init; }
}
