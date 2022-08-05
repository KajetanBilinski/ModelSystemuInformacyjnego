using MAS_2.Klasy;
using System;

namespace MAS_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Database();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(PracownikStudia.PRACOWNICY.Count);
            //Console.WriteLine();
            //foreach(var item in PracownikStudia.PRACOWNICY)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine();
            //Console.WriteLine(Rezyser.REZYSEROWIE.Count);
            //Console.WriteLine(Grafik.GRAFICY.Count);
            //Console.WriteLine(Montazysta.MONTAZYSCI.Count);
            //Console.WriteLine(Produkcja.PRODUKCJE.Count);
            //Console.WriteLine(FilmPelnometrazowy.FILMY.Count);
            //Console.WriteLine(MontazystaOdcinek.MONTAZYSTAODCINEK.Count);
            Console.WriteLine(Database.conn.State);
            Database.conn.Open();
            //Klient klient = new Klient("TEST", "999", "132131311", false);
            //klient.dodajZamowienie(new Zamowienie(klient, Produkcja.PRODUKCJE.Find(p => p.idProdukcja == 3),DateTime.Now, false));
            //Produkcja p = new Produkcja("TEST3", DateTime.Now, null, "cHUJOWY", 1999, false);
            //new PracownikStudia("Mateusz", "Skoneczny", DateTime.Now, DateTime.Now, 1, false);
            //Montazysta m =new Montazysta("Korniszon", "Umpo", DateTime.Now, DateTime.Now, 2, false);
            //m.dodajPseudonim(new PracownikStudia.Pseudonim("Kisiel",m,false));
            //Rezyser r = new Rezyser("TEST", "TEST", DateTime.Now, DateTime.Now, 2,DateTime.Now,"TEST",100, false);
            //Grafik g = new Grafik("Borys", "Szyc", DateTime.Now, DateTime.Now, 1000, "Sraki", false);
            //g.dodajGrafikOdcinek(new GrafikOdcinek(g, SerialAnimowany.Odcinek.ODCINKI.Find(o => o.idOdcinek == 1), false));
            //new RezyserProdukcja(r, p, false);
            //FilmPelnometrazowy fp = new FilmPelnometrazowy("TEST3", DateTime.Now, null, "cHUJOWY", 1999, 120, DateTime.Now, DateTime.Now, 100, false);
            //fp.dodajAnimowany(new FilmPelnometrazowy.Animowany("sraka 3d", false), false);
            //fp.dodajFabularny(new FilmPelnometrazowy.Fabularny(false), false);
            //FilmPelnometrazowy.usun(fp);
            // SerialAnimowany sa = new SerialAnimowany("TEST USUWANIA 1111", DateTime.Now, null, "bsi", 10000, 4, 20, "TEST",false);
            //sa.dodajOdcinek(new SerialAnimowany.Odcinek(2, "TEST USUWANIA 1111", "TEST", "TEST", sa, false));
            //Klient.usunKlienta(klient);
            //SerialAnimowany.usun(sa);
            //Grafik.usun(Grafik.GRAFICY.Find(g => g.idGrafik == 2));
            PracownikStudia pracownik = new PracownikStudia("a", "b", DateTime.Now, DateTime.UtcNow, 2332, false);
            pracownik.dodajPseudonim(new PracownikStudia.Pseudonim("Jakis", pracownik, false));
            PracownikStudia.usun(pracownik);

            Console.WriteLine("FILMY");
            Console.WriteLine(FilmPelnometrazowy.FILMY.Count);
            Console.WriteLine("GRAFICY");
            Console.WriteLine(Grafik.GRAFICY.Count);
            Console.WriteLine("MONTAZYSCI");
            Console.WriteLine(Montazysta.MONTAZYSCI.Count);
            Console.WriteLine("REZYSEROWIE");
            Console.WriteLine(Rezyser.REZYSEROWIE.Count);
            Console.WriteLine("PRACOWNICY");
            Console.WriteLine(PracownikStudia.PRACOWNICY.Count);
            Console.WriteLine("KLIENCI");
            Console.WriteLine(Klient.KLIENCI.Count);
            Console.WriteLine("GRAFIK ODCINEK");
            Console.WriteLine(GrafikOdcinek.GRAFIKODCINEK.Count);
            Console.WriteLine("MONTAZYSTA ODCINEK");
            Console.WriteLine(MontazystaOdcinek.MONTAZYSTAODCINEK.Count);
            Console.WriteLine("REZYSER PRODUKCJA");
            Console.WriteLine(RezyserProdukcja.REZYSERPRODUKCJA.Count);
            Console.WriteLine("PRODUKCJA");
            Console.WriteLine(Produkcja.PRODUKCJE.Count);
            Console.WriteLine("SERIALANIMOWANY");
            Console.WriteLine(SerialAnimowany.SERIALEANIMOWANE.Count);
            Console.WriteLine("ODCINKI");
            Console.WriteLine(SerialAnimowany.Odcinek.ODCINKI.Count);
            Console.WriteLine("FABULARNE");
            Console.WriteLine(FilmPelnometrazowy.Fabularny.FABULARNE.Count);
            Console.WriteLine("ANIMOWANE");
            Console.WriteLine(FilmPelnometrazowy.Animowany.ANIMOWANE.Count);
            Console.WriteLine("PSEUDONIMY");
            Console.WriteLine(PracownikStudia.Pseudonim.PSEUDONIMY.Count);
            Console.WriteLine("ZAMOWIENIE");
            Console.WriteLine(Zamowienie.ZAMOWIENIA.Count);

        }
    }
}
