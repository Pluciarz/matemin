using MatemIn.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace MatemIn.Backend.Data;

public class MatemInDbContext : DbContext
{
    public MatemInDbContext(DbContextOptions<MatemInDbContext> options) : base(options)
    {
    }

    public DbSet<Uzytkownik> Uzytkownicy => Set<Uzytkownik>();
    public DbSet<ZalogowanyUczen> Uczniowie => Set<ZalogowanyUczen>();
    public DbSet<Moderator> Moderatorzy => Set<Moderator>();
    public DbSet<Dzial> Dzialy => Set<Dzial>();
    public DbSet<Zadanie> Zadania => Set<Zadanie>();
    public DbSet<Rozwiazanie> Rozwiazania => Set<Rozwiazanie>();
    public DbSet<Komentarz> Komentarze => Set<Komentarz>();
    public DbSet<Ocena> Oceny => Set<Ocena>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Dziedziczenie TPH dla hierarchii Uzytkownik -> ZalogowanyUczen -> Moderator
        modelBuilder.Entity<Uzytkownik>()
            .HasDiscriminator<string>("Rola")
            .HasValue<ZalogowanyUczen>("Uczen")
            .HasValue<Moderator>("Moderator");

        // Hierarchia działów (Dzial -> Poddzialy)
        modelBuilder.Entity<Dzial>()
            .HasOne(d => d.Rodzic)
            .WithMany(d => d.Poddzialy)
            .HasForeignKey(d => d.RodzicId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacja Dzial -> Zadania
        modelBuilder.Entity<Zadanie>()
            .HasOne(z => z.Dzial)
            .WithMany(d => d.Zadania)
            .HasForeignKey(z => z.DzialId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relacja Autor -> Zadania
        modelBuilder.Entity<Zadanie>()
            .HasOne(z => z.Autor)
            .WithMany(u => u.Zadania)
            .HasForeignKey(z => z.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacja Zadanie -> Rozwiazania
        modelBuilder.Entity<Rozwiazanie>()
            .HasOne(r => r.Zadanie)
            .WithMany(z => z.Rozwiazania)
            .HasForeignKey(r => r.ZadanieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja Autor -> Rozwiazania
        modelBuilder.Entity<Rozwiazanie>()
            .HasOne(r => r.Autor)
            .WithMany(u => u.Rozwiazania)
            .HasForeignKey(r => r.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacja Rozwiazanie -> Komentarze
        modelBuilder.Entity<Komentarz>()
            .HasOne(k => k.Rozwiazanie)
            .WithMany(r => r.Komentarze)
            .HasForeignKey(k => k.RozwiazanieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja Autor -> Komentarze
        modelBuilder.Entity<Komentarz>()
            .HasOne(k => k.Autor)
            .WithMany(u => u.Komentarze)
            .HasForeignKey(k => k.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacja Rozwiazanie -> Oceny
        modelBuilder.Entity<Ocena>()
            .HasOne(o => o.Rozwiazanie)
            .WithMany(r => r.Oceny)
            .HasForeignKey(o => o.RozwiazanieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja Autor -> Oceny
        modelBuilder.Entity<Ocena>()
            .HasOne(o => o.Autor)
            .WithMany(u => u.Oceny)
            .HasForeignKey(o => o.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reguła biznesowa: jeden użytkownik może wystawić tylko jedną ocenę dla danego rozwiązania
        modelBuilder.Entity<Ocena>()
            .HasIndex(o => new { o.RozwiazanieId, o.AutorId })
            .IsUnique();
    }
}
