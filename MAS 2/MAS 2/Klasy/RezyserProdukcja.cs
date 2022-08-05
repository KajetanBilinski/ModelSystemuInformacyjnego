
using System.Collections.Generic;

namespace MAS_2.Klasy
{
    public class RezyserProdukcja
    {
        public int idProdukcja;
        public int idRezyser;
        public Rezyser rezyser;
        public Produkcja produkcja;

        public static List<RezyserProdukcja> REZYSERPRODUKCJA = new List<RezyserProdukcja>();

        public RezyserProdukcja(Rezyser rezyser, Produkcja produkcja, bool fromDatabase)
        {
            if(rezyser!=null && produkcja != null && !REZYSERPRODUKCJA.Contains(this))
            {
                REZYSERPRODUKCJA.Add(this);
                idProdukcja = produkcja.idProdukcja;
                idRezyser = rezyser.idRezyser;
                dodajProdukcje(produkcja);
                dodajRezysera(rezyser);
                if (!fromDatabase)
                {
                    Database.dodajRezyserProdukcje(this);
                }
            }
            else
            {
                System.Console.WriteLine("Error rezyser produkcja istnieje");
            }
            
        }

        public void dodajRezysera(Rezyser rezyser)
        {
            if(rezyser!=null && this.rezyser==null)
            {
                this.rezyser = rezyser;
                rezyser.dodajRezyserProdukcje(this);
            }
        }
        public void dodajProdukcje(Produkcja produkcja)
        {
            if (produkcja != null && this.produkcja == null)
            {
                this.produkcja = produkcja;
                produkcja.dodajRezyserProdukcje(this);
            }
        }

        public void usunProdukcje(Produkcja produkcja)
        {
            if (this.produkcja == produkcja && produkcja != null)
            {
                this.produkcja = null;
                produkcja.usunRezyserProdukcje(this);
            }
            if (this.rezyser != null)
            {
                usunRezysera(this.rezyser);
            }
            RezyserProdukcja.usun(this);
        }

        public void usunRezysera(Rezyser rezyser)
        {
            if (this.rezyser == rezyser && rezyser != null)
            {
                this.rezyser = null;
                rezyser.usunRezyserProdukcje(this);
            }
            if (this.produkcja != null)
            {
                usunProdukcje(this.produkcja);
            }
            RezyserProdukcja.usun(this);
        }

        public static void usun(RezyserProdukcja rezyserProdukcja)
        {
            if (REZYSERPRODUKCJA.Contains(rezyserProdukcja))
            {
                REZYSERPRODUKCJA.Remove(rezyserProdukcja);
                //Database.usunMontazystaOdcinek(rezyserProdukcja);
            }
        }


    }
}
