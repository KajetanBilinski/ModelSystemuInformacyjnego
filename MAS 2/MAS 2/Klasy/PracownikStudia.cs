using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class PracownikStudia
    {
        public int idPracStud;
        public string imie;
        public string nazwisko;
        public DateTime dataUrodzenia;
        public DateTime dataZatrudnienia;
        public double pensja;
        public int staz;
        public List<Pseudonim> pseudonimy;

        public static List<PracownikStudia> PRACOWNICY = new List<PracownikStudia>();

        public PracownikStudia(string imie, string nazwisko, DateTime dataUrodzenia, DateTime dataZatrudnienia, double pensja, bool fromDatabase)
        {
            if(!PRACOWNICY.Contains(this))
            {
                this.imie = imie;
                this.nazwisko = nazwisko;
                this.dataUrodzenia = dataUrodzenia;
                this.dataZatrudnienia = dataZatrudnienia;
                this.pensja = pensja;
                //this.staz = staz;
                pseudonimy = new List<Pseudonim>();
                PRACOWNICY.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajPracownikaStudia(this);
                }
            }
            else
            {
                Console.WriteLine("Error pracownik istnieje");
            }
            
        }
        public void dodajPseudonim(Pseudonim pseudonim)
        {
            if(pseudonim != null && !pseudonimy.Contains(pseudonim))
            {
                pseudonimy.Add(pseudonim);   
            }
        }
        public void usunPseudonim(Pseudonim pseudonim)
        {
            if(pseudonimy.Contains(pseudonim))
            {
                pseudonimy.Remove(pseudonim);
                Pseudonim.usun(pseudonim);
            }
        }

        public static void usun(PracownikStudia pracownikStudia)
        {
            if(PRACOWNICY.Contains(pracownikStudia))
            {
                PRACOWNICY.Remove(pracownikStudia);
                foreach(Pseudonim pseudonim in pracownikStudia.pseudonimy)
                {
                    Pseudonim.usun(pseudonim);
                }
                pracownikStudia.pseudonimy.Clear();
                Database.usunPracownikaStudia(pracownikStudia);
            }
        }



        public class Pseudonim
        {
            public int idPseud;
            public string nazwaPseud;
            public PracownikStudia pracownikStudia;

            public static List<Pseudonim> PSEUDONIMY = new List<Pseudonim>();

            public Pseudonim(string nazwaPseud,PracownikStudia pracownikStudia, bool fromDatabase)
            {
                if (!PSEUDONIMY.Contains(this))
                {
                    this.nazwaPseud = nazwaPseud;
                    this.pracownikStudia = pracownikStudia;
                    PSEUDONIMY.Add(this);
                    if (!fromDatabase)
                    {
                        Database.dodajPseudonim(this);
                    }
                }
                else
                {
                    Console.WriteLine("Erorr pseudonim istnieje");
                }            
            }
            public static void usun(Pseudonim pseudonim)
            {
                if(PSEUDONIMY.Contains(pseudonim))
                {
                    PSEUDONIMY.Remove(pseudonim);
                    Database.usunPseudonim(pseudonim);
                }
            }
        }
    }
}
