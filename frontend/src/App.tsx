import './App.css'
import { useMemo, useState } from 'react'
import { mockDzialy, mockZadania } from './mockData'
import type { Dzial } from './types'

const zakladki = [
  'Baza zadań',
  'Dodaj zadanie',
  'Kalkulator',
  'Logowanie',
] as const

type Zakladka = (typeof zakladki)[number]

interface DzialInfo {
  id: number
  nazwa: string
  pelnaNazwa: string
  rodzicId?: number
  wszystkieId: number[]
}

function odmianaZadania(liczba: number): string {
  if (liczba === 1) return 'zadanie'
  const r10 = liczba % 10
  const r100 = liczba % 100
  if (r10 >= 2 && r10 <= 4 && (r100 < 10 || r100 >= 20)) {
    return 'zadania'
  }
  return 'zadań'
}

function App() {
  const [aktywnaZakladka, setAktywnaZakladka] = useState<Zakladka>(zakladki[0])
  const [wybranyDzialId, setWybranyDzialId] = useState<number | null>(null)
  const [szukanaFraza, setSzukanaFraza] = useState<string>('')

  // Mapa działów i poddziałów do szybkiego wyszukiwania nazw oraz hierarchii ID
  const mapaDzialow = useMemo(() => {
    const mapa = new Map<number, DzialInfo>()

    const zarejestrujDzial = (dzial: Dzial, sciezkaRodzica?: string) => {
      const pelnaNazwa = sciezkaRodzica
        ? `${sciezkaRodzica} › ${dzial.nazwa}`
        : dzial.nazwa

      const dzieciId: number[] = []
      if (dzial.poddzialy) {
        for (const pod of dzial.poddzialy) {
          dzieciId.push(pod.id)
        }
      }

      mapa.set(dzial.id, {
        id: dzial.id,
        nazwa: dzial.nazwa,
        pelnaNazwa,
        rodzicId: dzial.rodzicId,
        wszystkieId: [dzial.id, ...dzieciId],
      })

      if (dzial.poddzialy) {
        for (const pod of dzial.poddzialy) {
          zarejestrujDzial(pod, dzial.nazwa)
        }
      }
    }

    for (const d of mockDzialy) {
      zarejestrujDzial(d)
    }

    return mapa
  }, [])

  // Licznik zadań przypisanych do danego działu lub jego poddziałów
  const pobierzLiczbeZadanDzialu = (dzialId: number): number => {
    const info = mapaDzialow.get(dzialId)
    if (!info) return 0
    return mockZadania.filter(
      (z) => z.dzialId !== undefined && info.wszystkieId.includes(z.dzialId),
    ).length
  }

  // Filtrowanie zadań po wybranym dziale i frazie tekstowej
  const przefiltrowaneZadania = useMemo(() => {
    const fraza = szukanaFraza.trim().toLowerCase()

    return mockZadania.filter((zadanie) => {
      const pasujeDzial =
        wybranyDzialId === null ||
        (zadanie.dzialId !== undefined &&
          (mapaDzialow.get(wybranyDzialId)?.wszystkieId.includes(zadanie.dzialId) ??
            false))

      const pasujeFraza =
        fraza === '' || zadanie.tresc.toLowerCase().includes(fraza)

      return pasujeDzial && pasujeFraza
    })
  }, [wybranyDzialId, szukanaFraza, mapaDzialow])

  const wyczyscFiltry = () => {
    setWybranyDzialId(null)
    setSzukanaFraza('')
  }

  return (
    <main className="app-container">
      <header className="app-header">
        <div className="app-header-content">
          <h1>MateMin</h1>
          <p className="app-header-subtitle">
            Baza zadań maturalnych i narzędzia do nauki matematyki
          </p>
        </div>
      </header>

      <nav className="app-nav" aria-label="Nawigacja główna">
        {zakladki.map((zakladka) => (
          <button
            key={zakladka}
            type="button"
            className={`nav-button ${aktywnaZakladka === zakladka ? 'nav-button-active' : ''}`}
            aria-current={aktywnaZakladka === zakladka ? 'page' : undefined}
            onClick={() => setAktywnaZakladka(zakladka)}
          >
            {zakladka}
          </button>
        ))}
      </nav>

      {aktywnaZakladka === 'Baza zadań' ? (
        <div className="baza-zadan-layout">
          <aside className="sidebar-dzialy" aria-label="Filtrowanie po działach">
            <div className="sidebar-header">
              <h2>Działy maturalne</h2>
            </div>

            <ul className="dzialy-lista">
              <li>
                <button
                  type="button"
                  className={`dzial-btn ${wybranyDzialId === null ? 'dzial-btn-active' : ''}`}
                  onClick={() => setWybranyDzialId(null)}
                >
                  <span className="dzial-btn-nazwa">Wszystkie działy</span>
                  <span className="dzial-badge">{mockZadania.length}</span>
                </button>
              </li>

              {mockDzialy.map((dzial) => {
                const czyAktywny = wybranyDzialId === dzial.id
                const liczbaZadan = pobierzLiczbeZadanDzialu(dzial.id)

                return (
                  <li key={dzial.id} className="dzial-grupa">
                    <button
                      type="button"
                      className={`dzial-btn ${czyAktywny ? 'dzial-btn-active' : ''}`}
                      onClick={() => setWybranyDzialId(dzial.id)}
                    >
                      <span className="dzial-btn-nazwa">{dzial.nazwa}</span>
                      <span className="dzial-badge">{liczbaZadan}</span>
                    </button>

                    {dzial.poddzialy && dzial.poddzialy.length > 0 && (
                      <ul className="poddzialy-lista">
                        {dzial.poddzialy.map((poddzial) => {
                          const czyPodAktywny = wybranyDzialId === poddzial.id
                          const liczbaPod = pobierzLiczbeZadanDzialu(poddzial.id)

                          return (
                            <li key={poddzial.id}>
                              <button
                                type="button"
                                className={`poddzial-btn ${czyPodAktywny ? 'poddzial-btn-active' : ''}`}
                                onClick={() => setWybranyDzialId(poddzial.id)}
                              >
                                <span className="poddzial-btn-nazwa">
                                  {poddzial.nazwa}
                                </span>
                                {liczbaPod > 0 && (
                                  <span className="poddzial-badge">
                                    {liczbaPod}
                                  </span>
                                )}
                              </button>
                            </li>
                          )
                        })}
                      </ul>
                    )}
                  </li>
                )
              })}
            </ul>
          </aside>

          <section className="glowna-zawartosc" aria-label="Lista zadań">
            <div className="wyszukiwarka-sekcja">
              <div className="input-szukaj-wrapper">
                <span className="szukaj-ikona" aria-hidden="true">
                  🔍
                </span>
                <input
                  type="search"
                  className="input-szukaj"
                  placeholder="Szukaj zadania po treści (np. funkcja kwadratowa, ciąg, kąt)..."
                  value={szukanaFraza}
                  onChange={(e) => setSzukanaFraza(e.target.value)}
                  aria-label="Wyszukaj zadanie po treści"
                />
                {szukanaFraza.trim() !== '' && (
                  <button
                    type="button"
                    className="btn-wyczysc-szukaj"
                    onClick={() => setSzukanaFraza('')}
                    aria-label="Wyczyść pole wyszukiwania"
                  >
                    ✕
                  </button>
                )}
              </div>

              <div className="filtry-status">
                <span className="liczba-wynikow">
                  Znaleziono: <strong>{przefiltrowaneZadania.length}</strong>{' '}
                  {odmianaZadania(przefiltrowaneZadania.length)}
                  {wybranyDzialId !== null && (
                    <span className="aktywny-filtr-tag">
                      Dział: {mapaDzialow.get(wybranyDzialId)?.pelnaNazwa}
                    </span>
                  )}
                </span>

                {(wybranyDzialId !== null || szukanaFraza.trim() !== '') && (
                  <button
                    type="button"
                    className="btn-reset-filtrow"
                    onClick={wyczyscFiltry}
                  >
                    Wyczyść filtry
                  </button>
                )}
              </div>
            </div>

            {przefiltrowaneZadania.length > 0 ? (
              <div className="lista-kart-zadan" role="list">
                {przefiltrowaneZadania.map((zadanie) => {
                  const infoDzialu = zadanie.dzialId
                    ? mapaDzialow.get(zadanie.dzialId)
                    : undefined

                  return (
                    <article
                      key={zadanie.id}
                      className="karta-zadania"
                      role="listitem"
                    >
                      <header className="karta-naglowek">
                        <div className="karta-naglowek-lewa">
                          <span className="zadanie-numer">
                            Zadanie #{zadanie.id}
                          </span>
                          <span className="dzial-tag">
                            {infoDzialu?.pelnaNazwa ?? 'Dział ogólny'}
                          </span>
                        </div>
                        <span className="status-tag status-zatwierdzone">
                          {zadanie.status}
                        </span>
                      </header>

                      <div className="karta-tresc">
                        <p>{zadanie.tresc}</p>
                      </div>

                      <footer className="karta-stopka">
                        <span className="zadanie-data">
                          Dodano: {zadanie.dataDodania}
                        </span>
                      </footer>
                    </article>
                  )
                })}
              </div>
            ) : (
              <div className="brak-wynikow">
                <div className="brak-wynikow-ikona" aria-hidden="true">
                  📐
                </div>
                <h3>Brak zadań spełniających kryteria</h3>
                <p>
                  Nie znaleziono zadań pasujących do wybranego działu lub wpisanej
                  frazy.
                </p>
                <button
                  type="button"
                  className="btn-reset-filtrow"
                  onClick={wyczyscFiltry}
                >
                  Pokaż wszystkie zadania
                </button>
              </div>
            )}
          </section>
        </div>
      ) : (
        <section className="app-content">
          <p>Widok {aktywnaZakladka} — w trakcie implementacji</p>
        </section>
      )}
    </main>
  )
}

export default App
