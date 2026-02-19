using System;
using System.Collections.Generic;
using System.Linq;

namespace TridniKnihaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vytvoření instance třídní knihy
            TridniKniha kniha = new TridniKniha();

            // Vytvoření studentů
            Student jan = new Student { Jmeno = "Jan", Prijmeni = "Novák" };
            Student eva = new Student { Jmeno = "Eva", Prijmeni = "Svobodová" };
            Student petr = new Student { Jmeno = "Petr", Prijmeni = "Dvořák" };

            // ======== Běžné testy ========

            Console.WriteLine("Test 1: Přidání docházky pro nového studenta");
            kniha.ZapisDochazku(jan, new DateTime(2026, 2, 19), true);
            Console.WriteLine(kniha.VypisDochazku(jan).Count == 1 ? "PASS" : "FAIL");

            Console.WriteLine("Test 2: Přepis docházky pro stejný den");
            kniha.ZapisDochazku(jan, new DateTime(2026, 2, 19), false);
            Console.WriteLine(kniha.VypisDochazku(jan)[0].Priznamka == false ? "PASS" : "FAIL");

            Console.WriteLine("Test 3: Výpis studenta bez záznamů");
            Console.WriteLine(kniha.VypisDochazku(eva).Count == 0 ? "PASS" : "FAIL");

            Console.WriteLine("Test 4: Více záznamů v různých dnech");
            kniha.ZapisDochazku(jan, new DateTime(2026, 2, 20), true);
            Console.WriteLine(kniha.VypisDochazku(jan).Count == 2 ? "PASS" : "FAIL");

            Console.WriteLine("Test 5: Přidání docházky pro jiného studenta");
            kniha.ZapisDochazku(petr, new DateTime(2026, 2, 19), true);
            Console.WriteLine(kniha.VypisDochazku(petr).Count == 1 ? "PASS" : "FAIL");

            Console.WriteLine("Test 6: Více studentů s více dny");
            kniha.ZapisDochazku(eva, new DateTime(2026, 2, 18), true);
            kniha.ZapisDochazku(eva, new DateTime(2026, 2, 19), true);
            Console.WriteLine(kniha.VypisDochazku(eva).Count == 2 ? "PASS" : "FAIL");

            // ======== Hraniční testy ========

            Console.WriteLine("Hraniční test 7: Null student");
            try
            {
                kniha.ZapisDochazku(null, DateTime.Now, true);
                Console.WriteLine("FAIL");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("PASS");
            }

            Console.WriteLine("Hraniční test 8: Velmi staré datum");
            kniha.ZapisDochazku(jan, new DateTime(1900, 1, 1), true);
            Console.WriteLine(kniha.VypisDochazku(jan).Any(d => d.Datum.Year == 1900) ? "PASS" : "FAIL");

            Console.WriteLine("\nVšechny testy dokončeny.");
        }
    }

    // ======== Třídy ========

    class Student
    {
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
    }

    class Dochazka
    {
        public DateTime Datum { get; set; }
        public bool Priznamka { get; set; } // true = přítomen
    }

    class TridniKniha
    {
        private Dictionary<Student, List<Dochazka>> zaznamy = new();

        public void ZapisDochazku(Student student, DateTime datum, bool priznamka)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));

            if (!zaznamy.ContainsKey(student))
                zaznamy[student] = new List<Dochazka>();

            var existujici = zaznamy[student].FirstOrDefault(d => d.Datum.Date == datum.Date);
            if (existujici != null)
                existujici.Priznamka = priznamka;
            else
            {
                zaznamy[student].Add(new Dochazka { Datum = datum, Priznamka = priznamka });
                zaznamy[student] = zaznamy[student].OrderBy(d => d.Datum).ToList();
            }
        }

        public List<Dochazka> VypisDochazku(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            return zaznamy.ContainsKey(student) ? zaznamy[student] : new List<Dochazka>();
        }
    }
}
