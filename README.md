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
- **Backend:** ASP.NET Core Web API (.NET 8, C#) + Entity Framework Core
- **Frontend:** React + TypeScript (Vite)
- **Baza danych:** SQLite
- **Integracje zewnętrzne:** Model LLM (kategoryzacja zadań), Wolfram Alpha API (kalkulator)

<!-- Po skonfigurowaniu pipeline'u CI/CD: dodać tu badge statusu, tak jak w OOBT_Pandas -->

## Zespół i podział ról
- **Magda Kaczorowska:** Backend — API i baza danych (ASP.NET Core Web API, model EF Core)
- **Jan Rampalski:** Frontend (React + TypeScript)
- **Adam Dąbrowski:** Backend — integracje zewnętrzne (Model LLM, Wolfram Alpha API) + repozytorium i CI/CD
- **Adam Lewandowski:** Backend — logika domenowa (moderacja, statusy zadań, zarządzanie kontami)
- **Maciej Radlak:** Testy i dokumentacja

## Dokumentacja projektu
Szczegółowe informacje znajdują się w folderze `docs/`:
- 📅 [Harmonogram prac](docs/harmonogram.md)
- 📋 [Wymagania funkcjonalne i niefunkcjonalne](docs/wymagania.md)
- 🗂️ [Diagramy (use case, klasy, sekwencji)](docs/diagrams/)

## Struktura repozytorium
```
matemin/
├── .github/
│   ├── ISSUE_TEMPLATE/
│   │   ├── bug.md
│   │   ├── config.yml
│   │   └── nowa-funkcjonalnosc.md
│   └── pull_request_template.md
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
├── backend/          # ASP.NET Core Web API (.NET 8 + EF Core + SQLite)
├── frontend/         # React + TypeScript (Vite)
├── .gitignore
└── README.md
```

## Uruchomienie projektu

**Wymagania:**
- .NET 8 SDK (backend)
- Node.js 18+ oraz npm (frontend)

### 1. Backend (ASP.NET Core Web API)
```bash
cd backend
dotnet restore
dotnet ef migrations add Init
dotnet ef database update
dotnet run
```
Po uruchomieniu API oraz dokumentacja Swagger UI będą dostępne pod adresem: `http://localhost:5000/swagger`.
Baza danych SQLite (`matemin.db`) zostanie utworzona automatycznie w katalogu `backend/`.

### 2. Frontend (React + Vite)
```bash
cd frontend
npm install
npm run dev
```
Aplikacja frontendowa będzie dostępna pod adresem: `http://localhost:5173`.

## Pipeline
<!-- TBD — do uzupełnienia po skonfigurowaniu CI/CD -->

## Komunikacja
- **Messenger:** Bieżąca komunikacja zespołu
- **GitHub Issues:** Zarządzanie zadaniami
- **Pull Requests:** Wprowadzanie zmian z obowiązkowym code review
