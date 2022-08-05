using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class Rezyser : PracownikStudia
    {
        public int idRezyser;
        public Certyfikat certyfikat;
        public List<RezyserProdukcja> produkcjeRezysera;
        public static int dodatekDoPensji;

        public static List<Rezyser> REZYSEROWIE = new List<Rezyser>();

        public Rezyser(string imie,string nazwisko, DateTime dataUrodzenia, DateTime dataZatrudnienia, double pensja,DateTime dataUzsykania,string nazwa,int dodatek, bool fromDatabase) 
            : base(imie, nazwisko, dataUrodzenia, dataZatrudnienia, pensja, fromDatabase)
        {
            if(!REZYSEROWIE.Contains(this))
            {
                this.certyfikat = new Certyfikat(nazwa, dataUzsykania);
                dodatekDoPensji = dodatek;
                produkcjeRezysera = new List<RezyserProdukcja>();
                REZYSEROWIE.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajRezysera(this);
                }
            }
            else
            {
                Console.WriteLine("Error rezyser istnieje");
            }
            
            
        }

        public void dodajRezyserProdukcje(RezyserProdukcja rezyserProdukcja)
        {
            if (!produkcjeRezysera.Contains(rezyserProdukcja))
            {
                produkcjeRezysera.Add(rezyserProdukcja);
                rezyserProdukcja.dodajRezysera(this);
            }
        }

        public void usunRezyserProdukcje(RezyserProdukcja rezyserProdukcja)
        {
            if (produkcjeRezysera.Contains(rezyserProdukcja))
            {
                produkcjeRezysera.Remove(rezyserProdukcja);
                rezyserProdukcja.usunRezysera(this);
            }
        }

        public static void usun(Rezyser rezyser)
        {
            if(REZYSEROWIE.Contains(rezyser))
            {
                REZYSEROWIE.Remove(rezyser);
                foreach(RezyserProdukcja rezyserProdukcja in rezyser.produkcjeRezysera)
                {
                    RezyserProdukcja.usun(rezyserProdukcja);
                }
                rezyser.produkcjeRezysera.Clear();
                Certyfikat.usun(rezyser.certyfikat);
                rezyser.certyfikat = null;
                Database.usunRezysera(rezyser);
            }
        }

    }
}
