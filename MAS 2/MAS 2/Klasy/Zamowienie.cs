using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class Zamowienie
    {
        public int idZamowienie;
        public DateTime dataZamowienia;
        public int idKlient;
        public int idProdukcja;
        public Klient klient;
        public Produkcja produkcja;

        public static List<Zamowienie> ZAMOWIENIA = new List<Zamowienie>();

        public Zamowienie(Klient klient, Produkcja produkcja, DateTime dataZamowienia, bool fromDatabase)
        {
            if(klient != null && produkcja!=null && !ZAMOWIENIA.Contains(this))
            {
                this.klient = klient;
                this.produkcja = produkcja;
                this.idProdukcja = produkcja.idProdukcja;
                this.idKlient = klient.idKlient;
                this.dataZamowienia = dataZamowienia;
                ZAMOWIENIA.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajZamowienie(this);
                }
            }
            else
            {
                Console.WriteLine("Zamowienie istnieje!");
            }
            
        }

        public void dodajKlienta(Klient klient)
        {
            if(klient!=null && this.klient==null)
            {
                this.klient = klient;
                klient.dodajZamowienie(this);
            }
        }

        public void dodajProdukcje(Produkcja produkcja)
        {
            if (produkcja != null && this.produkcja == null)
            {
                this.produkcja = produkcja;
                produkcja.dodajZamowienie(this);
            }
        }

        public void usunKlienta(Klient klient)
        {
            if(this.klient !=null && klient==null)
            {
                this.klient = null;
                klient.usunZamowienie(this);
            }
            if(this.produkcja!=null)
            {
                usunProdukcje(this.produkcja);
            }
            Zamowienie.usun(this);
        }
        
        public void usunProdukcje(Produkcja produkcja)
        {
            if(this.produkcja!=null && produkcja !=null)
            {
                this.produkcja = null;
                produkcja.usunZamowienie(this);
            }
            if(this.klient !=null)
            {
                usunKlienta(this.klient);
            }
            Zamowienie.usun(this);
        }

        public static void usun(Zamowienie zamowienie)
        {
            if(ZAMOWIENIA.Contains(zamowienie))
            {
                ZAMOWIENIA.Remove(zamowienie);
                Database.usunZamowienie(zamowienie);
            }
        }
    }
}
