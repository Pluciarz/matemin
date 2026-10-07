# Matemin

## Cel projektu
Celem projektu jest zaprojektowanie i implementacja aplikacji wspierającej
uczniów w przygotowaniach do matury z matematyki — bazy zadań maturalnych
wraz z rozwiązaniami, wzorowanej na matemaks.pl.

Projekt obejmuje:
- Bazę zadań maturalnych podzieloną na działy tematyczne
- Możliwość wyszukiwania i przeglądania zadań wg kryteriów
- Automatyczną kategoryzację zgłaszanych zadań przy pomocy modelu LLM
- Kalkulator matematyczny oparty o Wolfram Alpha API
- Konta użytkowników — komentowanie i ocenianie rozwiązań
- Moderację treści dodawanych przez użytkowników

## Technologie
- **Backend:** ASP.NET Core Web API (C#) + Entity Framework Core
- **Frontend:** React + TypeScript (Vite)
- **Baza danych:** SQLite
- **Integracje zewnętrzne:** Model LLM (kategoryzacja zadań), Wolfram Alpha API (kalkulator)

<!-- Po skonfigurowaniu pipeline'u CI/CD: dodać tu badge statusu, tak jak w OOBT_Pandas -->

## Zespół i podział ról
<!-- TBD — podział ról do ustalenia -->
- **Adam Lewandowski:**
- **Jan Rampalski:**
- **Maciej Radlak:**
- **Magda Kaczorowska:**
- **Adam Dąbrowski:**

## Dokumentacja projektu
Szczegółowe informacje znajdują się w folderze `docs/`:
- 📅 [Harmonogram prac](docs/harmonogram.md)
- 📋 [Wymagania funkcjonalne i niefunkcjonalne](docs/wymagania.md)
- 🗂️ [Diagramy (use case, klasy, sekwencji)](docs/diagrams/)

## Struktura repozytorium
```
matemin/
├── docs/
│   ├── diagrams/
│   │   ├── use-case-diagram.drawio
│   │   ├── use-case-diagram.png
│   │   ├── class-diagram.drawio
│   │   ├── class-diagram.png
│   │   ├── sequence-diagram.drawio
│   │   └── sequence-diagram.png
│   ├── harmonogram.md
│   └── wymagania.md
├── backend/          <!-- ASP.NET Core Web API -->
├── frontend/         <!-- React + TypeScript (Vite) -->
└── README.md
```
<!-- Foldery backend/ i frontend/ zostaną założone przy starcie implementacji -->

## Uruchomienie projektu
<!-- Zostanie uzupełnione konkretnymi krokami (dotnet run / npm run dev)
     po założeniu szkieletu backend/ i frontend/ -->
**Wymagania:**
- .NET SDK (backend)
- Node.js + npm (frontend)

Baza danych: SQLite — plik bazy tworzony lokalnie, bez dodatkowej konfiguracji.

## Pipeline
<!-- TBD — do uzupełnienia po skonfigurowaniu CI/CD -->

## Komunikacja
- **Messenger:** Bieżąca komunikacja zespołu
- **GitHub Issues:** Zarządzanie zadaniami
- **Pull Requests:** Wprowadzanie zmian z obowiązkowym code review
