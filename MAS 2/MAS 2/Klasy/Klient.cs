using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class Klient
    {
        public int idKlient;
        public string nazwaFirmy;
        public string NIP;
        public string telefon;
        public List<Zamowienie> zamowienia;

        public static List<Klient> KLIENCI = new List<Klient>();

        public Klient(string nazwaFirmy,string NIP,string telefon, bool fromDatabase)
        {
            if(!KLIENCI.Contains(this))
            {
                this.NIP = NIP;
                this.nazwaFirmy = nazwaFirmy;
                this.telefon = telefon;
                this.zamowienia=new List<Zamowienie>();
                KLIENCI.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajKlienta(this);
                }
            }
            else
            {
                Console.WriteLine("Error klient istnieje");
            }

        }

        public void dodajZamowienie(Zamowienie zamowienie)
        {
            if(!zamowienia.Contains(zamowienie))
            {
                zamowienia.Add(zamowienie);
                zamowienie.dodajKlienta(this);
            }
        }

        public void usunZamowienie(Zamowienie zamowienie)
        {
            if (zamowienia.Contains(zamowienie))
            {
                zamowienia.Remove(zamowienie);
                zamowienie.usunKlienta(this);
            }
        }

        public static void usunKlienta(Klient klient)
        {
            if(KLIENCI.Contains(klient))
            {
                KLIENCI.Remove(klient);
                foreach(Zamowienie zamowienie in klient.zamowienia)
                {
                    Zamowienie.usun(zamowienie);
                }
                Database.usunKlienta(klient);
            }
        }
       

    }
}
