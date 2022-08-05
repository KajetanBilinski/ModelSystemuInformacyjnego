using MAS_2.Klasy;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace MAS_2
{
    public class Database
    {
        public static readonly SQLiteConnection conn = new SQLiteConnection("Data Source = " + Directory.GetCurrentDirectory() + "/../../../MASBase.db");
       public Database()
       {
            
                conn.Open();
                wczytajPracownikowStudia();
                wczytajRezyserow();
                wczytajMontazystow(); 
                wczytajGrafikow();
                wczytajFilmy(); 
                wczytajSerialeAnimowane(); 
                wczytajKlientow();

                // ASOCJACYJNE
                wczytajMontazystaOdcinek();
                wczytajGrafikOdcinek();
                wczytajRezyserProdukcje();
                wczytajZamowienia();
                conn.Close();
                
            
       }
        public static void usunKlienta(Klient klient)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Klient WHERE idKlient={klient.idKlient}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunProdukcje(Produkcja produkcja)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                usunSerialeProdukcji(produkcja);
                cmd.CommandText = $"DELETE FROM Produkcja WHERE idProdukcja={produkcja.idProdukcja}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunPseudonim(PracownikStudia.Pseudonim pseudonim)
        {
            using(SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Pseudonim WHERE idPseud={pseudonim.idPseud}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunPracownikaStudia(PracownikStudia pracownikStudia)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM PracownikStudia WHERE idPracStud={pracownikStudia.idPracStud}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunRezysera(Rezyser rezyser)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Rezyser WHERE idRezyser={rezyser.idRezyser}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunSerialeProdukcji(Produkcja produkcja)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"SELECT idSerialAnimowany FROM SerialAnimowany WHERE idProdukcja_FK={produkcja.idProdukcja}";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        SerialAnimowany serialAnimowany = SerialAnimowany.SERIALEANIMOWANE.Find(s => s.idSerialAnimowany == results.GetInt32(0));
                        SerialAnimowany.usun(serialAnimowany);
                    }
                }
            }
        }
        public static void usunSerialAnimowany(SerialAnimowany serialAnimowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM SerialAnimowany WHERE idSerialAnimowany={serialAnimowany.idSerialAnimowany}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void usunOdcinek(SerialAnimowany.Odcinek odcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Odcinek WHERE idOdcinek={odcinek.idOdcinek}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void usunMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Montazysta_Odcinek WHERE idOdcinek_FK={montazystaOdcinek.idOdcinek} AND idMontazysta_FK = {montazystaOdcinek.idMontazysta}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunGrafikOdcinek(GrafikOdcinek grafikOdcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Grafik_Odcinek WHERE idOdcinek_FK={grafikOdcinek.idOdcinek} AND idGrafik_FK = {grafikOdcinek.idGrafik}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void usunAnimowany(FilmPelnometrazowy.Animowany animowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Animowany WHERE idAnimowany={animowany.idAnimowany}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void usunFabularny(FilmPelnometrazowy.Fabularny fabularny)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Fabularny WHERE idFabularny={fabularny.idFabularny}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunGrafika(Grafik grafik)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Grafik WHERE idGrafik={grafik.idGrafik}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunZamowienie(Zamowienie zamowienie)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Zamowienie WHERE idZamowienie={zamowienie.idZamowienie}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void usunKierownikaZOdcinka(SerialAnimowany.Odcinek odcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"UPDATE ODCINEK SET idMontazysta_FK = NULL WHERE idOdcinek={odcinek.idOdcinek}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void usunMontazyste(Montazysta montazysta)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM Montazysta WHERE idMontazysta={montazysta.idMontazysta}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajKlienta(Klient klient)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Klient", "idKlient");
                klient.idKlient = id;
                cmd.CommandText = $"INSERT INTO Klient VALUES ({klient.idKlient},'{klient.nazwaFirmy}','{klient.NIP}','{klient.telefon}')";
                cmd.ExecuteNonQuery();
            }
        }

      
        public static void dodajProdukcje(Produkcja produkcja)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Produkcja", "idProdukcja");
                produkcja.idProdukcja = id;
                if (produkcja.dataZakonczenia == null)
                {
                    cmd.CommandText = $"INSERT INTO Produkcja VALUES ({produkcja.idProdukcja},'{produkcja.tytul}','{produkcja.dataRozpoczecia}'," +
                    $"NULL,'{produkcja.status}','{produkcja.koszt}')";
                }
                
                else
                {
                    cmd.CommandText = $"INSERT INTO Produkcja VALUES ({produkcja.idProdukcja},'{produkcja.tytul}','{produkcja.dataRozpoczecia}'," +
                    $"'{produkcja.dataZakonczenia}','{produkcja.status}','{produkcja.koszt}')";
                }
                cmd.ExecuteNonQuery();
            }
        }
        public static void dodajPracownikaStudia(PracownikStudia pracownikStudia)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("PracownikStudia", "idPracStud");
                pracownikStudia.idPracStud = id;
                cmd.CommandText = $"INSERT INTO PracownikStudia VALUES ({pracownikStudia.idPracStud},'{pracownikStudia.imie}','{pracownikStudia.nazwisko}'," +
                    $"'{pracownikStudia.dataUrodzenia}','{pracownikStudia.dataZatrudnienia}','{pracownikStudia.pensja}')";
                cmd.ExecuteNonQuery();
                
            }
        }
        public static void dodajPseudonim(PracownikStudia.Pseudonim pseudonim)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Pseudonim", "idPseud");
                pseudonim.idPseud = id;
                cmd.CommandText = $"INSERT INTO Pseudonim VALUES ({pseudonim.idPseud},'{pseudonim.nazwaPseud}',{pseudonim.pracownikStudia.idPracStud})";
                cmd.ExecuteNonQuery();
            }
        }
        
        public static void dodajMontazyste(Montazysta montazysta)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Montazysta", "idMontazysta");
                montazysta.idMontazysta = id;
                cmd.CommandText = $"INSERT INTO Montazysta VALUES ({montazysta.idMontazysta},{montazysta.idPracStud})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajRezysera(Rezyser rezyser)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Rezyser", "idRezyser");
                rezyser.idRezyser = id;
                cmd.CommandText = $"INSERT INTO Rezyser VALUES ({rezyser.idRezyser},{rezyser.idPracStud},'{rezyser.certyfikat.dataOtrzymania}','{rezyser.certyfikat.nazwa}','{Rezyser.dodatekDoPensji}')";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajGrafika(Grafik grafik)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Grafik", "idGrafik");
                grafik.idGrafik = id;
                cmd.CommandText = $"INSERT INTO Grafik VALUES ({grafik.idGrafik},'{grafik.linkDoPortfolio}',{grafik.idPracStud})";
                cmd.ExecuteNonQuery();
            }
        }
        public static void dodajAnimowany(FilmPelnometrazowy.Animowany animowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Animowany", "idAnimowany");
                animowany.idAnimowany = id;
                cmd.CommandText = $"INSERT INTO Animowany VALUES ({animowany.idAnimowany},'{animowany.rodzajAnimacji}')";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajFabularny(FilmPelnometrazowy.Fabularny fabularny)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Fabularny", "idFabularny");
                fabularny.idFabularny = id;
                cmd.CommandText = $"INSERT INTO Fabularny VALUES ({fabularny.idFabularny})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajRezyserProdukcje(RezyserProdukcja rezyserProdukcja)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO Rezyser_Produkcja VALUES ({rezyserProdukcja.idProdukcja},{rezyserProdukcja.idRezyser})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajMontazystaOdcinek(MontazystaOdcinek montazystaOdcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO Montazysta_Odcinek VALUES ({montazystaOdcinek.idMontazysta},{montazystaOdcinek.idOdcinek})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajGrafikOdcinek(GrafikOdcinek grafikOdcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO Grafik_Odcinek VALUES ({grafikOdcinek.idGrafik},{grafikOdcinek.idOdcinek})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajOdcinek(SerialAnimowany.Odcinek odcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Odcinek", "idOdcinek");
                odcinek.idOdcinek = id;
                if(odcinek.kierownik == null)
                    cmd.CommandText = $"INSERT INTO Odcinek VALUES ({odcinek.idOdcinek},{odcinek.numer},'{odcinek.tytul}','{odcinek.krotkiOpis}','{odcinek.dlugiOpis}',{odcinek.serialAnimowany.idSerialAnimowany},NULL)";
                else
                    cmd.CommandText = $"INSERT INTO Odcinek VALUES ({odcinek.idOdcinek},{odcinek.numer},'{odcinek.tytul}','{odcinek.krotkiOpis}','{odcinek.dlugiOpis}',{odcinek.serialAnimowany.idSerialAnimowany}',{odcinek.kierownik.idMontazysta})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajFilmPelnometrazowy(FilmPelnometrazowy film)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                if (!SerialAnimowany.Odcinek.ODCINKI.Any(odc => odc.tytul.Equals(odc.tytul)))
                {
                    int id = getMaxIdFromTable("FilmPelnometrazowy", "idFilmPelnometrazowy");
                    film.idFilmPelnometrazowy = id;
                    if(film.fabularny==null)
                    {
                        if (film.animowany==null)
                        {
                            //oba null
                            Console.WriteLine(1);
                            cmd.CommandText = $"INSERT INTO FilmPelnometrazowy VALUES ({film.idFilmPelnometrazowy},{film.dlugosc},'{film.dataWypuszczeniaDoKin}','{film.dataWypuszczeniaDVD}'" +
                                $",{film.cenaBiletu},{film.idProdukcja},NULL,NULL)";
                        }
                        else
                        {
                            Console.WriteLine(2);
                            cmd.CommandText = $"INSERT INTO FilmPelnometrazowy VALUES ({film.idFilmPelnometrazowy},{film.dlugosc},'{film.dataWypuszczeniaDoKin}','{film.dataWypuszczeniaDVD}'" +
                                $",{film.cenaBiletu},{film.idProdukcja},{film.idAnimowany},NULL)";
                        }
                        // 1 null
                    }
                    else if(film.animowany==null)
                    {
                        // 1 null
                        Console.WriteLine(2);
                        cmd.CommandText = $"INSERT INTO FilmPelnometrazowy VALUES ({film.idFilmPelnometrazowy},{film.dlugosc},'{film.dataWypuszczeniaDoKin}','{film.dataWypuszczeniaDVD}'" +
                                $",{film.cenaBiletu},{film.idProdukcja},NULL,{film.idFabularny})";
                    }
                    else
                    {
                        // 2 nie null
                        Console.WriteLine(2);
                        cmd.CommandText = $"INSERT INTO FilmPelnometrazowy VALUES ({film.idFilmPelnometrazowy},{film.dlugosc},'{film.dataWypuszczeniaDoKin}','{film.dataWypuszczeniaDVD}'" +
                                $",{film.cenaBiletu},{film.idProdukcja},{film.idAnimowany},{film.idFabularny})";
                    }
                    cmd.ExecuteNonQuery();    
                }
            }
        }
        public static void dodajZamowienie(Zamowienie zamowienie)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("Zamowienie", "idZamowienie");
                zamowienie.idZamowienie = id;
                cmd.CommandText = $"INSERT INTO Zamowienie VALUES ({zamowienie.idZamowienie},'{zamowienie.dataZamowienia}',{zamowienie.idKlient},{zamowienie.idProdukcja})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajSerialAnimowany(SerialAnimowany serialAnimowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                int id = getMaxIdFromTable("SerialAnimowany", "idSerialAnimowany");
                serialAnimowany.idSerialAnimowany = id;
                cmd.CommandText = $"INSERT INTO SerialAnimowany VALUES ({serialAnimowany.idSerialAnimowany},{serialAnimowany.iloscSezonow},{serialAnimowany.dlugoscOdcinka}," +
                    $"'{serialAnimowany.tempoWypuszczaniaOdcinkow}',{serialAnimowany.idProdukcja})";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajFabularnyDoFilmu(FilmPelnometrazowy film,FilmPelnometrazowy.Fabularny fabularny)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"UPDATE FilmPelnometrazowy SET idFabularny_FK = {fabularny.idFabularny} WHERE idFilmPelnometrazowy = {film.idFilmPelnometrazowy}";
                cmd.ExecuteNonQuery();
            }
        }
        public static void dodajAnimowanyDoFilmu(FilmPelnometrazowy film, FilmPelnometrazowy.Animowany animowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"UPDATE FilmPelnometrazowy SET idAnimowany_FK = {animowany.idAnimowany} WHERE idFilmPelnometrazowy = {film.idFilmPelnometrazowy}";
                cmd.ExecuteNonQuery();
            }
        }

        public static void dodajKierownikaDoOdcinka(Montazysta kierownik,SerialAnimowany.Odcinek odcinek)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"UPDATE Odcinek SET idMontazysta_FK = {kierownik.idMontazysta} WHERE idOdcinek = {odcinek.idOdcinek}";
                cmd.ExecuteNonQuery();
            }
        }

        

        private static int getMaxIdFromTable(string tableName, string idName)
        {
            int id=0;
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"SELECT MAX({idName})+1 FROM {tableName}";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        id = results.GetInt32(0); 
                    }
                }
                else
                {
                    return 1;
                }
            }
            return id;
        }

        private void wczytajZamowienia()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idZamowienie,DataZamowienia,idProdukcja_FK,idKlient_FK FROM Zamowienie";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetString(1));
                        Console.WriteLine(results.GetInt32(2));
                        Console.WriteLine(results.GetInt32(3));
                        Produkcja produkcja = Produkcja.PRODUKCJE.Find(p => p.idProdukcja == results.GetInt32(2));
                        Klient klient = Klient.KLIENCI.Find(k => k.idKlient == results.GetInt32(3));
                        Zamowienie zamowienie = new Zamowienie(klient, produkcja,DateTime.Parse(results.GetString(1)), true);
                        zamowienie.idProdukcja = results.GetInt32(2);
                        zamowienie.idKlient = results.GetInt32(3);
                    }
                }
            }
        }

        private void wczytajRezyserProdukcje()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idProdukcja_FK,idRezyser_FK FROM Rezyser_Produkcja";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Produkcja produkcja = Produkcja.PRODUKCJE.Find(p => p.idProdukcja == results.GetInt32(0));
                        Rezyser rezyser = Rezyser.REZYSEROWIE.Find(r => r.idRezyser == results.GetInt32(1));
                        RezyserProdukcja rezyserProdukcja = new RezyserProdukcja(rezyser, produkcja, true);
                        rezyserProdukcja.idProdukcja = results.GetInt32(0);
                        rezyserProdukcja.idRezyser = results.GetInt32(1);
                    }
                }
            }
        }

        private void wczytajGrafikOdcinek()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idGrafik_FK,idOdcinek_FK FROM Grafik_Odcinek";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Grafik grafik = Grafik.GRAFICY.Find(g => g.idGrafik == results.GetInt32(0));
                        SerialAnimowany.Odcinek odcinek = SerialAnimowany.Odcinek.ODCINKI.Find(o => o.idOdcinek == results.GetInt32(1));
                        GrafikOdcinek grafikOdcinek = new GrafikOdcinek(grafik, odcinek, true);
                        grafikOdcinek.idGrafik = results.GetInt32(0);
                        grafikOdcinek.idOdcinek = results.GetInt32(1);
                    }
                }
            }
        }

        private void wczytajMontazystaOdcinek()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idMontazysta_FK,idOdcinek_FK FROM Montazysta_Odcinek";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Montazysta montazysta = Montazysta.MONTAZYSCI.Find(m => m.idMontazysta == results.GetInt32(0));
                        SerialAnimowany.Odcinek odcinek = SerialAnimowany.Odcinek.ODCINKI.Find(o => o.idOdcinek == results.GetInt32(1));
                        MontazystaOdcinek montazystaOdcinek = new MontazystaOdcinek(montazysta, odcinek,true);
                        montazystaOdcinek.idMontazysta = results.GetInt32(0);
                        montazystaOdcinek.idOdcinek = results.GetInt32(1);
                    }
                }
            }
        }

        private void wczytajKlientow()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idKlient,NazwaFirmy,NIP,Telefon FROM Klient";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetString(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Klient klient = new Klient(results.GetString(1), results.GetString(2), results.GetString(3), true);
                        klient.idKlient = results.GetInt32(0);
                    }
                }
            }
        }

        private void wczytajSerialeAnimowane()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idProdukcja_FK,idSerialAnimowany,Tytul,DataRozpoczecia,DataZakonczenia,Status,Koszt," +
                    "IloscSezonow,DlugoscOdcinka,TempoWypuszczaniaOdcinkow FROM SerialAnimowany JOIN Produkcja ON idProdukcja = idProdukcja_FK";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0)); //idProdukcja_FK
                        Console.WriteLine(results.GetInt32(1)); //idSerialAnimowany
                        Console.WriteLine(results.GetString(2)); //Tytul
                        Console.WriteLine(results.GetString(3)); //DataRozpoczecia
                        Console.WriteLine(DBNull.Value == results[4] ? "NULL" : results.GetString(4)); //DataZakonczenia
                        Console.WriteLine(results.GetString(5)); //Status
                        Console.WriteLine(results.GetInt32(6)); //Koszt
                        Console.WriteLine(results.GetInt32(7)); //IloscSezonow
                        Console.WriteLine(results.GetInt32(8)); // DlugoscOdcinka
                        Console.WriteLine(results.GetString(9)); // tempo

                        SerialAnimowany serialAnimowany = new SerialAnimowany(results.GetString(2), DateTime.Parse(results.GetString(3)),
                            results[4] == DBNull.Value ? null : DateTime.Parse(results.GetString(4)), results.GetString(5), results.GetInt32(6), results.GetInt32(7), results.GetInt32(8), results.GetString(9), true);
 
                        serialAnimowany.idProdukcja = results.GetInt32(0);
                        serialAnimowany.idSerialAnimowany = results.GetInt32(1);

                        wczytajOdcinki(results.GetInt32(1), serialAnimowany);
                    }
                }
            }
        }

        private void wczytajOdcinki(int idSerialAnimowany,SerialAnimowany serialAnimowany)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idOdcinek,idSerialAnimowany_FK,Numer,Tytul,KrotkiOpis,DlugiOpis,idMontazysta_FK FROM Odcinek WHERE idSerialAnimowany_FK = " + idSerialAnimowany;
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Console.WriteLine(results.GetInt32(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(results.GetString(4));
                        Console.WriteLine(results.GetString(5));
                        SerialAnimowany.Odcinek odcinek = null;
                        Console.WriteLine();
                        if (DBNull.Value == results[6])
                        {
                            odcinek = new SerialAnimowany.Odcinek(results.GetInt32(2), results.GetString(3), results.GetString(4), results.GetString(5), serialAnimowany, true);
                            Console.WriteLine("NULL");
                            odcinek.idOdcinek = results.GetInt32(0);    
                        }
                        else
                        {
                            Console.WriteLine(results.GetInt32(6));
                            Montazysta kierownik = Montazysta.MONTAZYSCI.Find(m => m.idMontazysta == results.GetInt32(6));
                            odcinek = new SerialAnimowany.Odcinek(results.GetInt32(2), results.GetString(3), results.GetString(4), results.GetString(5), serialAnimowany, kierownik, true);
                            
                        }           
                        odcinek.idOdcinek = results.GetInt32(0);
                    }
                }
            }
        }

        private void wczytajFilmy()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idProdukcja_FK,idFilmPelnometrazowy,Tytul,DataRozpoczecia,DataZakonczenia,Status,Koszt," +
                    "Dlugosc,DataWypuszczeniaDoKin,DataWypuszczeniaDVD,CenaBiletu,idAnimowany_FK,idFabularny_FK FROM FilmPelnometrazowy JOIN Produkcja ON idProdukcja = idProdukcja_FK";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(DBNull.Value == results[4] ? "NULL":results.GetString(4));
                        Console.WriteLine(results.GetString(5));
                        Console.WriteLine(results.GetInt32(6));
                        Console.WriteLine(results.GetInt32(7));
                        Console.WriteLine(results.GetString(8));
                        Console.WriteLine(results.GetString(9));
                        Console.WriteLine(results.GetDouble(10));

                        FilmPelnometrazowy film = new FilmPelnometrazowy(results.GetString(2), DateTime.Parse(results.GetString(3)),
                            results[4] == DBNull.Value ? null : DateTime.Parse(results.GetString(4)), results.GetString(5), results.GetInt32(6),results.GetInt32(7),
                            DateTime.Parse(results.GetString(8)), DateTime.Parse(results.GetString(9)), results.GetDouble(10), true);

                        film.idProdukcja = results.GetInt32(0);
                        film.idFilmPelnometrazowy = results.GetInt32(1);

                        if (DBNull.Value == results[11])
                        {
                            Console.WriteLine("NULL");
                        }
                        else
                        {
                            Console.WriteLine(results.GetInt32(11));
                            wczytajAnimowane(results.GetInt32(11), film);
                            film.idAnimowany = results.GetInt32(11);
                            film.animowany.idAnimowany = results.GetInt32(11);
                        }
                        if (DBNull.Value == results[12]) 
                        {
                            Console.WriteLine("NULL");
                        }
                        else
                        {
                            Console.WriteLine(results.GetInt32(12));
                            FilmPelnometrazowy.Fabularny fabularny = new FilmPelnometrazowy.Fabularny(true);
                            fabularny.idFabularny = results.GetInt32(12);
                            film.dodajFabularny(fabularny, true);
                            film.idFabularny = results.GetInt32(12);
                            film.fabularny.idFabularny= results.GetInt32(12);
                        }

                    }
                }
            }
        }

        private void wczytajAnimowane(int idAnimowany, FilmPelnometrazowy film)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT rodzajAnimacji FROM Animowany WHERE idAnimowany = " + idAnimowany;
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetString(0));
                        film.dodajAnimowany(new FilmPelnometrazowy.Animowany(results.GetString(0), true), true);
                    }
                }
            }
        }

        private void wczytajGrafikow()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idPracStud,idGrafik,imie,nazwisko,DataUrodzenia,DataZatrudnienia,Pensja,linkDoPortfolio FROM PracownikStudia JOIN Grafik ON idPracStud = idPracStud_FK ";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(results.GetString(4));
                        Console.WriteLine(results.GetString(5));
                        Console.WriteLine(results.GetDouble(6));
                        Console.WriteLine(results.GetString(7));

                        Grafik grafik = new Grafik(results.GetString(2), results.GetString(3),
                        DateTime.Parse(results.GetString(4)), DateTime.Parse(results.GetString(5)), results.GetDouble(6),results.GetString(7), true);
                        wczytajPseudonimy(results.GetInt32(0), grafik);
                        grafik.idGrafik= results.GetInt32(1);
                        grafik.idPracStud = results.GetInt32(0);
                    }
                }
            }
        }

        private void wczytajMontazystow()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idPracStud,idMontazysta,imie,nazwisko,DataUrodzenia,DataZatrudnienia,Pensja FROM PracownikStudia JOIN Montazysta ON idPracStud = idPracStud_FK ";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(results.GetString(4));
                        Console.WriteLine(results.GetString(5));
                        Console.WriteLine(results.GetDouble(6));

                        Montazysta montazysta = new Montazysta(results.GetString(2), results.GetString(3),
                        DateTime.Parse(results.GetString(4)), DateTime.Parse(results.GetString(5)), results.GetDouble(6), true);
                        wczytajPseudonimy(results.GetInt32(0), montazysta);
                        montazysta.idMontazysta = results.GetInt32(1);
                        montazysta.idPracStud = results.GetInt32(0);
                    }
                }
            }
        }

        private void wczytajRezyserow()
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idPracStud,idRezyser,imie,nazwisko,DataUrodzenia,DataZatrudnienia,Pensja,dataUzyskania,nazwaCertyfikatu,dodatekDoPensji FROM PracownikStudia JOIN Rezyser ON idPracStud = idPracStud_FK ";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetInt32(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(results.GetString(4));
                        Console.WriteLine(results.GetString(5));
                        Console.WriteLine(results.GetDouble(6));
                        Console.WriteLine(results.GetString(7));
                        Console.WriteLine(results.GetString(8));
                        Console.WriteLine(results.GetInt32(9));
                        Rezyser rezyser = new Rezyser(results.GetString(2), results.GetString(3),
                        DateTime.Parse(results.GetString(4)), DateTime.Parse(results.GetString(5)), results.GetDouble(6), DateTime.Parse(results.GetString(7)), 
                        results.GetString(8), results.GetInt32(9), true);
                        wczytajPseudonimy(results.GetInt32(0), rezyser);
                        rezyser.idRezyser = results.GetInt32(1);
                        rezyser.idPracStud = results.GetInt32(0);
                       
                    }
                }
            }
        }

        public void wczytajPracownikowStudia()
       {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idPracStud,imie,nazwisko,DataUrodzenia,DataZatrudnienia,Pensja FROM PracownikStudia " +
                    "WHERE idPracStud not in (SELECT idPracStud_FK FROM Rezyser) " +
                    "AND idPracStud not in (SELECT idPracStud_FK FROM Montazysta)" +
                    "AND idPracStud not in (SELECT idPracStud_FK FROM Grafik)";
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while(results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetString(1));
                        Console.WriteLine(results.GetString(2));
                        Console.WriteLine(results.GetString(3));
                        Console.WriteLine(results.GetString(4));
                        Console.WriteLine(results.GetDouble(5));                      
                        PracownikStudia pracownik = new PracownikStudia(results.GetString(1), results.GetString(2), 
                        DateTime.Parse(results.GetString(3)), DateTime.Parse(results.GetString(4)),results.GetDouble(5), true);
                        wczytajPseudonimy(results.GetInt32(0),pracownik);
                        pracownik.idPracStud = results.GetInt32(0);
                    }
                }
            }
        }
        public void wczytajPseudonimy(int id,PracownikStudia pracownik)
        {
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT idPseud , NazwaPseud FROM PSEUDONIM WHERE idPracStud_FK = "+id;
                var results = cmd.ExecuteReader();
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        Console.WriteLine(results.GetInt32(0));
                        Console.WriteLine(results.GetString(1));
                        PracownikStudia.Pseudonim pseudonim = new PracownikStudia.Pseudonim(results.GetString(1),pracownik, true);
                        pseudonim.idPseud = results.GetInt32(0);
                        pracownik.dodajPseudonim(pseudonim);
                    }
                }
            }
        }
    }
}
