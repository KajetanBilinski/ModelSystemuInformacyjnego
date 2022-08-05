using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAS_2.Klasy.SerialAnimowany;

namespace MAS_2.Klasy
{
    public class GrafikOdcinek
    {
        public int idGrafik;
        public int idOdcinek;
        public Odcinek odcinek;
        public Grafik grafik;

        public static List<GrafikOdcinek> GRAFIKODCINEK = new List<GrafikOdcinek>();    

        public GrafikOdcinek(Grafik grafik, Odcinek odcinek, bool fromDatabase)
        {
            if (grafik != null && odcinek != null && !GRAFIKODCINEK.Contains(this))
            {
                GRAFIKODCINEK.Add(this);
                idGrafik = grafik.idGrafik;
                idOdcinek = odcinek.idOdcinek;
                dodajGrafika(grafik);
                dodajOdcinek(odcinek);
                if (!fromDatabase)
                {
                    Database.dodajGrafikOdcinek(this);
                }
            }
            else
            {
                Console.WriteLine("Erorr grafik odcinek istnieje");
            }
          
        }

        public void dodajGrafika(Grafik grafik)
        {
            if(grafik != null && this.grafik == null)
            {
                this.grafik = grafik;
                grafik.dodajGrafikOdcinek(this);
            }
        }

        public void dodajOdcinek(Odcinek odcinek)
        {
            if(odcinek != null && this.odcinek == null)
            {
                this.odcinek = odcinek;
                odcinek.dodajGrafikOdcinek(this);
            }
        }
        
        public void usunGrafika(Grafik grafik)
        {
            if(this.grafik != null && grafik == null)
            {
                this.grafik = null;
                grafik.usunGrafikOdcinek(this);
            }
            if(this.odcinek != null)
            {
                usunOdcinek(this.odcinek);
            }
            GrafikOdcinek.usun(this);
        }

        public void usunOdcinek(Odcinek odcinek)
        {
            if (this.odcinek != null && odcinek == null)
            {
                this.odcinek = null;
                odcinek.usunGrafikOdcinek(this);
            }
            if (this.grafik != null)
            {
                usunGrafika(this.grafik);
            }
            GrafikOdcinek.usun(this);
        }

        public static void usun(GrafikOdcinek grafikOdcinek)
        {
            if (GRAFIKODCINEK.Contains(grafikOdcinek))
            {
                GRAFIKODCINEK.Remove(grafikOdcinek);
                Database.usunGrafikOdcinek(grafikOdcinek);
            }
        }
    }
}
