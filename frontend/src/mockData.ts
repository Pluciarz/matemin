import type { Dzial, Zadanie } from './types'

export const mockDzialy: Dzial[] = [
  {
    id: 1,
    nazwa: 'Funkcje',
    poddzialy: [
      { id: 11, nazwa: 'Funkcja liniowa', rodzicId: 1 },
      { id: 12, nazwa: 'Funkcja kwadratowa', rodzicId: 1 },
      { id: 13, nazwa: 'Funkcja wykładnicza i logarytmiczna', rodzicId: 1 },
    ],
  },
  {
    id: 2,
    nazwa: 'Ciągi',
    poddzialy: [
      { id: 21, nazwa: 'Ciąg arytmetyczny', rodzicId: 2 },
      { id: 22, nazwa: 'Ciąg geometryczny', rodzicId: 2 },
    ],
  },
  {
    id: 3,
    nazwa: 'Trygonometria',
    poddzialy: [
      { id: 31, nazwa: 'Tożsamości trygonometryczne', rodzicId: 3 },
      { id: 32, nazwa: 'Równania i nierówności trygonometryczne', rodzicId: 3 },
    ],
  },
  {
    id: 4,
    nazwa: 'Planimetria',
    poddzialy: [
      { id: 41, nazwa: 'Trójkąty i twierdzenie Pitagorasa', rodzicId: 4 },
      { id: 42, nazwa: 'Czworokąty i okręgi', rodzicId: 4 },
    ],
  },
]

export const mockZadania: Zadanie[] = [
  {
    id: 1,
    tresc:
      'Wyznacz największą i najmniejszą wartość funkcji kwadratowej f(x) = 2x² - 8x + 5 w przedziale domkniętym ⟨-1, 3⟩.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-10',
    dzialId: 12,
    autorId: 101,
  },
  {
    id: 2,
    tresc:
      'Wyznacz wzór funkcji liniowej f(x) = ax + b, której wykres przechodzi przez punkty A = (-2, 6) oraz B = (4, -3). Oblicz jej miejsce zerowe.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-12',
    dzialId: 11,
    autorId: 102,
  },
  {
    id: 3,
    tresc:
      'W ciągu arytmetycznym (aₙ) dane są wyrazy a₃ = 7 oraz a₈ = 22. Oblicz pierwszy wyraz a₁, różnicę r oraz sumę pierwszych 20 wyrazów tego ciągu.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-15',
    dzialId: 21,
    autorId: 101,
  },
  {
    id: 4,
    tresc:
      'Dany jest rosnący ciąg geometryczny (aₙ), w którym a₁ = 3 oraz a₃ = 12. Wyznacz iloraz q tego ciągu oraz oblicz sumę jego pierwszych sześciu wyrazów.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-18',
    dzialId: 22,
    autorId: 103,
  },
  {
    id: 5,
    tresc:
      'Kąt α jest ostry i spełnia warunek sin α + cos α = 7/5. Oblicz wartość wyrażenia sin α · cos α oraz oblicz wartość tg α + ctg α.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-20',
    dzialId: 31,
    autorId: 104,
  },
  {
    id: 6,
    tresc:
      'W trójkącie prostokątnym ABC przyprostokątne mają długości |AC| = 6 oraz |BC| = 8. Oblicz promień okręgu wpisanego w ten trójkąt oraz promień okręgu opisanego na tym trójkącie.',
    status: 'ZATWIERDZONE',
    dataDodania: '2024-03-22',
    dzialId: 41,
    autorId: 105,
  },
]
