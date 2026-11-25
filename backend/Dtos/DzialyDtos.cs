namespace MateMin.Backend.Dtos;

public record PoddzialDto(int Id, string Nazwa, int? RodzicId);

public record DzialDto(
    int Id,
    string Nazwa,
    IReadOnlyList<PoddzialDto> Poddzialy
);
