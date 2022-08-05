using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_2.Klasy
{
    public class SerialAnimowany : Produkcja
    {
        public int idSerialAnimowany;
        public int iloscSezonow;
        public int dlugoscOdcinka;
        public string tempoWypuszczaniaOdcinkow;
        private Dictionary<string,Odcinek> odcinki;

        public static List<SerialAnimowany> SERIALEANIMOWANE = new List<SerialAnimowany>();

        public SerialAnimowany(string tytul, DateTime dataRozpoczecia, DateTime? dataZakonczenia, string status,int koszt,
            int iloscSezonow,int dlugoscOdcinka,string tempoWypuszczaniaOdcinkow, bool fromDatabase) : base(tytul,dataRozpoczecia,dataZakonczenia, status, koszt, fromDatabase)
        {
            if(!SERIALEANIMOWANE.Contains(this))
            {
                this.iloscSezonow = iloscSezonow;
                this.dlugoscOdcinka = dlugoscOdcinka;
                this.tempoWypuszczaniaOdcinkow = tempoWypuszczaniaOdcinkow;
                this.odcinki = new Dictionary<string, Odcinek>();
                SERIALEANIMOWANE.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajSerialAnimowany(this);
                }
            }
            else
            {
                Console.WriteLine("Error serial istnieje");
            }
           
        }

        public void dodajOdcinek(Odcinek odcinek)
        {
            if(odcinek!=null && !odcinki.ContainsKey(odcinek.tytul))
            {
                odcinki.Add(odcinek.tytul, odcinek);
            }
        }
        public void usunOdcinek(string tytul)
        {
            if(odcinki.ContainsKey(tytul))
            {
                Odcinek.usun(odcinki[tytul]);
                odcinki.Remove(tytul);
            }
        }

        public static void usun(SerialAnimowany serialAnimowany)
        {
            if(SERIALEANIMOWANE.Contains(serialAnimowany))
            {
                SERIALEANIMOWANE.Remove(serialAnimowany);
                foreach(Odcinek odcinek in serialAnimowany.odcinki.Values)
                {
                    Odcinek.usun(odcinek);
                }
                serialAnimowany.odcinki.Clear();
                Database.usunProdukcje(serialAnimowany);
            }
        }


        public class Odcinek : IComparable<Odcinek>
        {
            public int idOdcinek;
            public int idSerialAnimowany;
            public int numer;
            public string tytul;
            public string krotkiOpis;
            public string dlugiOpis;
            public SerialAnimowany serialAnimowany;
            public List<MontazystaOdcinek> montazysciOdcinka;
            public List<GrafikOdcinek> graficyOdcinka;
            public Montazysta kierownik;

            public static List<Odcinek> ODCINKI = new List<Odcinek>();

            public Odcinek(int numer, string tytul, string krotkiOpis, string dlugiOpis,SerialAnimowany serialAnimowany, bool fromDatabase)
            {
                if(!ODCINKI.Contains(this) && !ODCINKI.Any(odc=> odc.tytul.Equals(tytul)))
                {
                    this.serialAnimowany = serialAnimowany;
                    this.numer = numer;
                    this.tytul = tytul;
                    this.krotkiOpis = krotkiOpis;
                    this.dlugiOpis = dlugiOpis;
                    this.montazysciOdcinka = new List<MontazystaOdcinek>();
                    this.graficyOdcinka = new List<GrafikOdcinek>();
                    ODCINKI.Add(this);
                    ODCINKI.Sort();
                    serialAnimowany.dodajOdcinek(this);
                    if (!fromDatabase)
                    {
                        Database.dodajOdcinek(this);
                    }
                }
                else
                {
                    Console.WriteLine("Error odcinek istnieje");
                }
                
            }
            public Odcinek(int numer, string tytul, string krotkiOpis, string dlugiOpis, SerialAnimowany serialAnimowany,Montazysta kierownik, bool fromDatabase)
            {
                if (!ODCINKI.Contains(this) && !ODCINKI.Any(odc => odc.tytul.Equals(tytul)))
                {
                    this.serialAnimowany = serialAnimowany;
                    this.kierownik = kierownik;
                    this.numer = numer;
                    this.tytul = tytul;
                    this.krotkiOpis = krotkiOpis;
                    this.dlugiOpis = dlugiOpis;
                    this.montazysciOdcinka = new List<MontazystaOdcinek>();
                    this.graficyOdcinka = new List<GrafikOdcinek>();
                    ODCINKI.Add(this);
                    ODCINKI.Sort();
                    serialAnimowany.dodajOdcinek(this);
                    if (!fromDatabase)
                    {
                        Database.dodajOdcinek(this);
                    }
                }
                else
                {
                    Console.WriteLine("Error odcinek istnieje");
                }
               
            }


            public void dodajMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
            {
                if (!montazysciOdcinka.Contains(montazystaOdcinek))
                {
                    montazysciOdcinka.Add(montazystaOdcinek);
                    montazystaOdcinek.dodajOdcinek(this);
                }
            }

            public void usunMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
            {
                if (montazysciOdcinka.Contains(montazystaOdcinek))
                {
                    montazysciOdcinka.Remove(montazystaOdcinek);
                    montazystaOdcinek.usunOdcinek(this);
                }
            }

            public void dodajGrafikOdcinek(GrafikOdcinek grafikOdcinek)
            {
                if (!graficyOdcinka.Contains(grafikOdcinek))
                {
                    graficyOdcinka.Add(grafikOdcinek);
                    grafikOdcinek.dodajOdcinek(this);
                }
            }

            public void usunGrafikOdcinek(GrafikOdcinek grafikOdcinek)
            {
                if (graficyOdcinka.Contains(grafikOdcinek))
                {
                    graficyOdcinka.Remove(grafikOdcinek);
                    grafikOdcinek.usunOdcinek(this);
                }
            }

            public void dodajKierownika(Montazysta kierownik) // zawsze jest tylko od strony programu więc nie trzeba robić fromDatabase
            {
                if(kierownik!=null && this.kierownik==null)
                {
                    this.kierownik = kierownik;
                    kierownik.dodajOdcinekKierowany(this);
                    Database.dodajKierownikaDoOdcinka(kierownik, this);
                }
            }
            public void usunKierownika(Montazysta kierownik) // zawsze jest tylko od strony programu więc nie trzeba robić fromDatabase
            {
                if (kierownik != null && this.kierownik == null)
                {
                    this.kierownik = null;
                    kierownik.usunOdcinekKierowany(this);
                    Database.usunKierownikaZOdcinka(this);
                }
            }
            public static void usun(Odcinek odcinek)
            {
                if(ODCINKI.Contains(odcinek))
                {
                    ODCINKI.Remove(odcinek);
                    ODCINKI.Sort();
                    foreach (MontazystaOdcinek montazystaOdcinek in odcinek.montazysciOdcinka)
                    {
                        montazystaOdcinek.usunOdcinek(odcinek);
                    }
                    odcinek.montazysciOdcinka.Clear();
                    foreach(GrafikOdcinek grafikOdcinek in odcinek.graficyOdcinka)
                    {
                        grafikOdcinek.usunOdcinek(odcinek);
                    }
                    odcinek.graficyOdcinka.Clear();
                    Database.usunOdcinek(odcinek);
                }
                
            }

           

            public int CompareTo(Odcinek other)
            {
                return other.numer.CompareTo(this.numer);
            }
        }

        
    }
    
}
