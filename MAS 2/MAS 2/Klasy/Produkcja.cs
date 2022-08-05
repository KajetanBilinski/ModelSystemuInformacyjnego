using System;
using System.Collections.Generic;

namespace MAS_2.Klasy
{
    public class Produkcja
    {
        public int idProdukcja;
        public string tytul;
        public DateTime dataRozpoczecia;
        public DateTime? dataZakonczenia;
        public string status;
        public int koszt;
        public List<Zamowienie> zamowienia;
        public List<RezyserProdukcja> rezyserowieProdukcji;

        public static List<Produkcja> PRODUKCJE = new List<Produkcja>(); // do zastanowienia bo abstract

        public Produkcja(string tytul,DateTime dataRozpoczecia, DateTime? dataZakonczenia, string status,int koszt, bool fromDatabase)
        {
            if(!PRODUKCJE.Contains(this))
            {
                this.tytul = tytul;
                this.dataRozpoczecia = dataRozpoczecia;
                this.dataZakonczenia = dataZakonczenia;
                this.status = status;
                this.koszt = koszt;
                this.rezyserowieProdukcji = new List<RezyserProdukcja>();
                PRODUKCJE.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajProdukcje(this);
                }
            }
            else
            {
                Console.WriteLine("Error produkcja istnieje");
            }     
        }

        public void dodajRezyserProdukcje(RezyserProdukcja rezyserProdukcja)
        {
            if (!rezyserowieProdukcji.Contains(rezyserProdukcja))
            {
                rezyserowieProdukcji.Add(rezyserProdukcja);
                rezyserProdukcja.dodajProdukcje(this);
            }
        }

        public void usunRezyserProdukcje(RezyserProdukcja rezyserProdukcja)
        {
            if (rezyserowieProdukcji.Contains(rezyserProdukcja))
            {
                rezyserowieProdukcji.Remove(rezyserProdukcja);
                rezyserProdukcja.usunProdukcje(this);
            }
        }

        public void dodajZamowienie(Zamowienie zamowienie)
        {
            if (!zamowienia.Contains(zamowienie))
            {
                zamowienia.Add(zamowienie);
                zamowienie.dodajProdukcje(this);
            }
        }

        public void usunZamowienie(Zamowienie zamowienie)
        {
            if (zamowienia.Contains(zamowienie))
            {
                zamowienia.Remove(zamowienie);
                zamowienie.usunProdukcje(this);
            }
        }
        public static void usunProdukcje(Produkcja produkcja)
        {
            if(PRODUKCJE.Contains(produkcja))
            {
                PRODUKCJE.Remove(produkcja);
                foreach (Zamowienie zamowienie in produkcja.zamowienia)
                {
                    produkcja.usunZamowienie(zamowienie);
                }
                produkcja.zamowienia.Clear();
                foreach(RezyserProdukcja rezyserProdukcja in produkcja.rezyserowieProdukcji)
                {
                    produkcja.usunRezyserProdukcje(rezyserProdukcja);
                }
                produkcja.rezyserowieProdukcji.Clear();
                Database.usunProdukcje(produkcja);
            }
        }
    }
}
