using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class Grafik : PracownikStudia
    {
        public int idGrafik;
        public string linkDoPortfolio;
        public List<GrafikOdcinek> odcinkiGrafika;

        public static List<Grafik> GRAFICY = new List<Grafik>();

        public Grafik(string imie,string nazwisko, DateTime dataUrodzenia, DateTime dataZatrudnienia, double pensja,string linkDoPortfolio, bool fromDatabase) 
            : base(imie, nazwisko, dataUrodzenia, dataZatrudnienia, pensja,fromDatabase)
        {
            if(!GRAFICY.Contains(this))
            {
                this.linkDoPortfolio = linkDoPortfolio;
                this.odcinkiGrafika = new List<GrafikOdcinek>();
                GRAFICY.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajGrafika(this);
                }
            }
            else
            {
                Console.WriteLine("Error grafik istnieje");
            }
           
        }

        public void dodajGrafikOdcinek(GrafikOdcinek grafikOdcinek)
        {
            if (!odcinkiGrafika.Contains(grafikOdcinek))
            {
                odcinkiGrafika.Add(grafikOdcinek);
                grafikOdcinek.dodajGrafika(this);
            }
        }

        public void usunGrafikOdcinek(GrafikOdcinek grafikOdcinek)
        {
            if (odcinkiGrafika.Contains(grafikOdcinek))
            {
                odcinkiGrafika.Remove(grafikOdcinek);
                grafikOdcinek.usunGrafika(this);
            }
        }

        public static void usun(Grafik grafik)
        {
            if(GRAFICY.Contains(grafik))
            {
                GRAFICY.Remove(grafik);
                foreach(GrafikOdcinek grafikOdcinek in grafik.odcinkiGrafika)
                {
                    GrafikOdcinek.usun(grafikOdcinek);
                }
                grafik.odcinkiGrafika.Clear();
                Database.usunGrafika(grafik);
            }
        }
    }
}
