# Wymagania

## Wymagania funkcjonalne

Na podstawie diagramu przypadków użycia.

### Uczeń (Gość)
- Przeglądanie bazy zadań i działów
- Wyszukiwanie zadań po kryteriach
- Wrzucenie własnego zadania (upload)
- Rejestracja i logowanie (z podaniem daty urodzenia)

### Zalogowany Uczeń
- Dodawanie komentarza do rozwiązania
- Ocenianie rozwiązania
- Edycja/usunięcie własnego zgłoszonego zadania
- Przeglądanie własnego profilu i historii
- Wylogowanie

### Autor / Moderator
- Dodawanie nowych zadań i rozwiązań do bazy
- Zarządzanie bazą pytań i działów
- Zarządzanie kontami użytkowników

### Automatyzacja / integracje zewnętrzne
- Automatyczna kategoryzacja zgłoszonego zadania przy pomocy modelu LLM
- Dopasowanie rozwiązań z bazy na podstawie kategoryzacji
- Wykonywanie obliczeń przez kalkulator oparty o Wolfram Alpha API

### Reguły biznesowe
- Jeden użytkownik może wystawić tylko jedną ocenę dla danego rozwiązania.
- Zadanie zgłoszone przez ucznia ma status `OCZEKUJĄCE` do czasu decyzji moderatora
  (`ZATWIERDZONE` / `ODRZUCONE`) — założenie przyjęte przy diagramie klas i sekwencji,
  wymaga potwierdzenia.

## Wymagania niefunkcjonalne

- **Wydajność:**
  - Czas odpowiedzi endpointów REST API (.NET 8) dla zapytań odczytujących bazę zadań i działów nie powinien przekraczać 200 ms przy wykorzystaniu asynchronicznych zapytań EF Core (`async/await`) oraz projekcji danych (`Select`, `AsNoTracking`).
  - Zapytania do zewnętrznych serwisów (model LLM, Wolfram Alpha API) muszą być obsługiwane asynchronicznie z zaimplementowanym mechanizmem *timeout* (np. 10 sekund) oraz pamięcią podręczną (*In-Memory Caching*), aby nie blokować wątków serwera podczas kategoryzacji zadań.
  - Interfejs użytkownika (React + TypeScript) powinien wykorzystywać stronicowanie (paginację) wyników wyszukiwania oraz leniwe ładowanie komponentów (*lazy loading*), zapewniając płynne renderowanie listy zadań i wzorów matematycznych.

- **Bezpieczeństwo (hasła, dane osobowe uczniów):**
  - Hasła użytkowników muszą być haszowane przed zapisem do bazy danych przy użyciu bezpiecznych algorytmów kryptograficznych (np. PBKDF2 w ramach ASP.NET Core Identity lub BCrypt), a autoryzacja dostępu do ról (`Uczeń`, `Moderator`) powinna opierać się na tokenach JWT przesyłanych wyłącznie przez protokół HTTPS.
  - Dane osobowe (w tym data urodzenia wymagana przy rejestracji) muszą być chronione zgodnie z RODO (zasada minimalizacji danych), a klucze dostępowe do zewnętrznych API (LLM, Wolfram Alpha) przechowywane wyłącznie po stronie backendu w zmiennych środowiskowych lub *Secret Managerze*.
  - Pliki przesyłane przez użytkowników (upload zadań przez gości i uczniów) muszą przechodzić rygorystyczną walidację po stronie serwera pod kątem rozmiaru (np. maks. 5 MB), typu MIME (wyłącznie dozwolone formaty obrazów/PDF) oraz zabezpieczenia przed atakami *Path Traversal* i *XSS*.

- **Dostępność / responsywność (urządzenia mobilne):**
  - Aplikacja kliencka w React musi być zaprojektowana zgodnie z podejściem *Responsive Web Design (RWD)* / *Mobile-First*, zapewniając wygodne przeglądanie zadań, czytanie wzorów matematycznych oraz dodawanie zdjęć zadań bezpośrednio z poziomu smartfonów (rozdzielczości od 320px wzwyż).
  - Interfejs powinien spełniać podstawowe wytyczne standardu WCAG 2.1 (poziom AA), gwarantując odpowiedni kontrast tekstu, obsługę nawigacji z klawiatury oraz teksty alternatywne dla grafik z treścią zadań.
  - Komponenty formularzy (wyszukiwarka, upload zadań, kalkulator) powinny posiadać czytelne komunikaty walidacyjne i stany ładowania (*loading skeletons / spinners*) podczas oczekiwania na odpowiedź z API lub analizę przez model LLM.

- **Skalowalność bazy zadań:**
  - Warstwa dostępu do danych oparta na Entity Framework Core i wzorcu migracji musi być odseparowana od logiki biznesowej, co umożliwi bezproblemową migrację z lokalnej bazy plikowej SQLite (włączony tryb `WAL - Write-Ahead Logging` dla lepszej współbieżności odczytów) na docelowy silnik serwerowy (np. PostgreSQL lub MS SQL Server) wraz ze wzrostem ruchu przed maturami.
  - Baza danych powinna posiadać skonfigurowane indeksy na kolumnach najczęściej wykorzystywanych do filtrowania i wyszukiwania zadań (np. identyfikator działu, status zadania `OCZEKUJĄCE`/`ZATWIERDZONE`, tagi kategoryzacji).
  - Przesyłane pliki graficzne z zadaniami nie powinny być zapisywane jako obiekty `BLOB` wewnątrz pliku bazy SQLite, lecz przechowywane w wydzielonym systemie plików lub zewnętrznym magazynie obiektowym (np. AWS S3 / Azure Blob Storage), a w bazie powinny znajdować się jedynie ścieżki (URL) do zasobów.

## Planowane działania
- OCR (automatyczne wykrywanie obliczeń wykonanych ręcznie przez użytkownika)
