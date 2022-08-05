using System;
using System.Collections.Generic;

namespace MAS_2.Klasy
{
    public class Montazysta : PracownikStudia
    {
        public int idMontazysta;
        public List<MontazystaOdcinek> odcinkiMontazysty;
        public List<SerialAnimowany.Odcinek> odcinkiKierowane;

        public static List<Montazysta> MONTAZYSCI= new List<Montazysta>();

        public Montazysta(string imie, string nazwisko, DateTime dataUrodzenia, DateTime dataZatrudnienia, double pensja, bool fromDatabase) 
            : base(imie, nazwisko, dataUrodzenia, dataZatrudnienia, pensja,fromDatabase)
        {
            if(!MONTAZYSCI.Contains(this))
            {
                MONTAZYSCI.Add(this);
                odcinkiMontazysty = new List<MontazystaOdcinek>();
                odcinkiKierowane = new List<SerialAnimowany.Odcinek>();
                if (!fromDatabase)
                {
                    Database.dodajMontazyste(this);
                }
            }
            else
            {
                Console.WriteLine("Error montazysta istnieje");
            }
            
        }

        public void dodajMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
        {
            if(!odcinkiMontazysty.Contains(montazystaOdcinek))
            {
                odcinkiMontazysty.Add(montazystaOdcinek);
                montazystaOdcinek.dodajMontazyste(this);
            }
        }

        public void usunMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
        {
            if (odcinkiMontazysty.Contains(montazystaOdcinek))
            {
                odcinkiMontazysty.Remove(montazystaOdcinek);
                montazystaOdcinek.usunMontazyste(this);
            }
        }

        public void dodajOdcinekKierowany(SerialAnimowany.Odcinek odcinek)
        {
            if(odcinek!=null && odcinkiMontazysty.Exists(om=>om.odcinek.Equals(odcinek)) && !odcinkiKierowane.Contains(odcinek))
            {
                odcinkiKierowane.Add(odcinek);
                odcinek.dodajKierownika(this);
            }
        }
        public void usunOdcinekKierowany(SerialAnimowany.Odcinek odcinek)
        {
            if (odcinek != null && odcinkiKierowane.Contains(odcinek))
            {
                odcinkiKierowane.Remove(odcinek);
                odcinek.usunKierownika(this);
            }
        }
        public static void usun(Montazysta montazysta)
        {
            if(MONTAZYSCI.Contains(montazysta))
            {
                MONTAZYSCI.Remove(montazysta);
                foreach (SerialAnimowany.Odcinek odcinkiKierowane in montazysta.odcinkiKierowane)
                {
                    odcinkiKierowane.usunKierownika(montazysta);
                }
                montazysta.odcinkiKierowane.Clear();
                foreach (MontazystaOdcinek odcinkiMontowane in montazysta.odcinkiMontazysty)
                {
                    MontazystaOdcinek.usun(odcinkiMontowane);
                }
                montazysta.odcinkiMontazysty.Clear();
                Database.usunMontazyste(montazysta);
            }
        }
    }
}
