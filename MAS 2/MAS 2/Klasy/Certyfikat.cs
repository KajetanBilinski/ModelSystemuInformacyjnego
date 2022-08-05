using System;
using System.Collections.Generic;


namespace MAS_2.Klasy
{
    public class Certyfikat
    {
        public string nazwa;
        public DateTime dataOtrzymania;

        public static List<Certyfikat> CERTYFIKATY = new List<Certyfikat>();

        public Certyfikat(string nazwa, DateTime dataOtrzymania)
        {
            if(!CERTYFIKATY.Contains(this))
            {
                this.nazwa = nazwa;
                this.dataOtrzymania = dataOtrzymania;
                CERTYFIKATY.Add(this);
            }
            else
            {
                Console.WriteLine("Error certyfikat istnieje");
            }
        }
        public static void usun(Certyfikat certyfikat)
        {
            if(CERTYFIKATY.Contains(certyfikat))
            {
                CERTYFIKATY.Remove(certyfikat);
            }
        }
    }
}
