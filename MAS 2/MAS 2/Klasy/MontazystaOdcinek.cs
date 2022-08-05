using System.Collections.Generic;
using static MAS_2.Klasy.SerialAnimowany;

namespace MAS_2.Klasy
{
    public class MontazystaOdcinek
    {
        public int idMontazysta;
        public int idOdcinek;
        public Montazysta montazysta;
        public Odcinek odcinek;

        public static List<MontazystaOdcinek> MONTAZYSTAODCINEK = new List<MontazystaOdcinek>();

        public MontazystaOdcinek(Montazysta montazysta,Odcinek odcinek,bool fromDatabase)
        {
            if(montazysta!=null && odcinek!=null && !MONTAZYSTAODCINEK.Contains(this))
            {
                MONTAZYSTAODCINEK.Add(this);
                idMontazysta = montazysta.idMontazysta;
                idOdcinek = odcinek.idOdcinek;
                dodajMontazyste(montazysta);
                dodajOdcinek(odcinek);
                if (!fromDatabase)
                {
                    Database.dodajMontazystaOdcinek(this);
                }
            }
            else
            {
                System.Console.WriteLine("Erorr montazysta odcinek istnieje");
            }
            
        }

        public void dodajMontazyste(Montazysta montazysta)
        {
            if(montazysta!=null && this.montazysta==null)
            {
                this.montazysta = montazysta;
                montazysta.dodajMontazystaOdcinek(this);
            }
        }

        public void dodajOdcinek(Odcinek odcinek)
        {
            if(odcinek != null && this.odcinek == null)
            {
                this.odcinek = odcinek;
                odcinek.dodajMontazystaOdcinek(this);
            }
        }
        
        public void usunMontazyste(Montazysta montazysta)
        {
            if(this.montazysta != null && montazysta!=null)
            {
                this.montazysta = null;
                montazysta.usunMontazystaOdcinek(this);
            }
            if(this.odcinek != null)
            {
                usunOdcinek(this.odcinek);
            }
            MontazystaOdcinek.usun(this);
        }

        public void usunOdcinek(Odcinek odcinek)
        {
            if (this.odcinek != null && odcinek != null)
            {
                this.odcinek = null;
                odcinek.usunMontazystaOdcinek(this);
            }
            if (this.montazysta != null)
            {
                usunMontazyste(montazysta);
            }
            MontazystaOdcinek.usun(this);
        }

        public static void usun(MontazystaOdcinek montazystaOdcinek)
        {
            if(MONTAZYSTAODCINEK.Contains(montazystaOdcinek))
            {
                MONTAZYSTAODCINEK.Remove(montazystaOdcinek);
                Database.usunMontazystaOdcinek(montazystaOdcinek);
            }
        }
        
    }
}
