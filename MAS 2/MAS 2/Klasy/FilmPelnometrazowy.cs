using System;
using System.Collections.Generic;


namespace MAS_2.Klasy
{
    public class FilmPelnometrazowy : Produkcja
    {
        public int idFilmPelnometrazowy;
        public int dlugosc;
        public DateTime dataWypuszczeniaDoKin;
        public DateTime dataWypuszczeniaDVD;
        public double cenaBiletu;

        public int? idAnimowany;
        public int? idFabularny;
        public Fabularny? fabularny;
        public Animowany? animowany;

        public static List<FilmPelnometrazowy> FILMY = new List<FilmPelnometrazowy>();

        public FilmPelnometrazowy(string tytul, DateTime dataRozpoczecia, DateTime? dataZakonczenia, string status,int koszt,
            int dlugosc,DateTime dataWypuszczeniaDoKin,DateTime dataWypuszczeniaDVD,double cenaBiletu, bool fromDatabase) 
            : base(tytul, dataRozpoczecia, dataZakonczenia, status,koszt,fromDatabase)
        {
            if(!FILMY.Contains(this))
            {
                this.dlugosc = dlugosc;
                this.dataWypuszczeniaDoKin = dataWypuszczeniaDoKin;
                this.dataWypuszczeniaDVD = dataWypuszczeniaDVD;
                this.cenaBiletu = cenaBiletu;
                FILMY.Add(this);
                if (!fromDatabase)
                {
                    Database.dodajFilmPelnometrazowy(this);
                }
            }
            else
            {
                Console.WriteLine("Error film istnieje");
            }
            
        }

        public void dodajAnimowany(Animowany animowany,bool fromDatabase)
        {
            if(this.animowany == null && animowany!=null)
            {
                this.animowany = animowany;
                if (!fromDatabase)
                {
                    Database.dodajAnimowanyDoFilmu(this, animowany);
                }
            }
            
        }

        public void dodajFabularny(Fabularny fabularny, bool fromDatabase)
        {
            if (this.fabularny == null && fabularny != null)
            {
                this.fabularny = fabularny;
                if (!fromDatabase)
                {
                    Database.dodajFabularnyDoFilmu(this, fabularny);
                }
            }   
        }

        public static void usun(FilmPelnometrazowy filmPelnometrazowy)
        {
            if(FILMY.Contains(filmPelnometrazowy))
            {
                FILMY.Remove(filmPelnometrazowy);
                Fabularny.usun(filmPelnometrazowy.fabularny);
                Animowany.usun(filmPelnometrazowy.animowany);
                Database.usunProdukcje(filmPelnometrazowy);
            }
        }


        public class Animowany
        {
            public int idAnimowany;
            public string rodzajAnimacji;

            public static List<Animowany> ANIMOWANE = new List<Animowany>();

            public Animowany(string rodzajAnimacji, bool fromDatabase)
            {
                if(!ANIMOWANE.Contains(this))
                {
                    this.rodzajAnimacji = rodzajAnimacji;
                    ANIMOWANE.Add(this);
                    if (!fromDatabase)
                    {
                        Database.dodajAnimowany(this);
                    }
                }
                else
                {
                    Console.WriteLine("Error Animowany istnieje");
                }         
                
            }

            public static void usun(Animowany animowany)
            {
                if(ANIMOWANE.Contains(animowany))
                {
                    ANIMOWANE.Remove(animowany);
                    Database.usunAnimowany(animowany);
                }
            }
        }

        public class Fabularny
        {
            public int idFabularny;

            public static List<Fabularny> FABULARNE = new List<Fabularny>();

            public Fabularny(bool fromDatabase)
            {
                if (!FABULARNE.Contains(this))
                {
                    FABULARNE.Add(this);
                    if (!fromDatabase)
                    {
                        Database.dodajFabularny(this);
                    }
                }
                else
                {
                    Console.WriteLine("Error Fabularny istnieje");
                }
                
            }
            public static void usun(Fabularny fabularny)
            {
                if (FABULARNE.Contains(fabularny))
                {
                    FABULARNE.Remove(fabularny);
                    Database.usunFabularny(fabularny);
                }
            }
        }

        
    }
}
