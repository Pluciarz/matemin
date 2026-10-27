import './App.css'
import { useState } from 'react'

const zakladki = [
  'Baza zadań',
  'Dodaj zadanie',
  'Kalkulator',
  'Logowanie',
] as const

function App() {
  const [aktywnaZakladka, setAktywnaZakladka] = useState<(typeof zakladki)[number]>(
    zakladki[0],
  )

  return (
    <main className="app-container">
      <header className="app-header">
        <h1>Matemin</h1>
      </header>
      <nav aria-label="Nawigacja główna">
        {zakladki.map((zakladka) => (
          <button
            key={zakladka}
            type="button"
            aria-current={aktywnaZakladka === zakladka ? 'page' : undefined}
            onClick={() => setAktywnaZakladka(zakladka)}
          >
            {zakladka}
          </button>
        ))}
      </nav>
      <section className="app-content">
        <p>Widok {aktywnaZakladka} — w trakcie implementacji</p>
      </section>
    </main>
  )
}

export default App
