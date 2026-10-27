export type StatusZadania =
  | 'OCZEKUJACE'
  | 'ZATWIERDZONE'
  | 'ODRZUCONE'

export interface Dzial {
  id: number
  nazwa: string
  rodzicId?: number
  poddzialy?: Dzial[]
}

export interface Zadanie {
  id: number
  tresc: string
  status: StatusZadania
  dataDodania: string
  dzialId?: number
  autorId: number
}

export interface Rozwiazanie {
  id: number
  zadanieId: number
  tresc: string
  dataDodania: string
  autorId: number
  sredniaOcena?: number
}

export interface Komentarz {
  id: number
  rozwiazanieId: number
  tresc: string
  dataDodania: string
  autorId: number
}

export interface Ocena {
  id: number
  rozwiazanieId: number
  wartosc: number
  dataDodania: string
  autorId: number
}
