using System;

namespace AgregaceAKompozice
{
    public class Student
    {
        public string Jmeno { get; }
        public string Prijmeni { get; }
        public int Rocnik { get; }

        public Student(string jmeno, string prijmeni, int rocnik)
        {
            if (string.IsNullOrWhiteSpace(jmeno))
                throw new ArgumentException("Jméno nesmí být prázdné.", nameof(jmeno));

            if (string.IsNullOrWhiteSpace(prijmeni))
                throw new ArgumentException("Příjmení nesmí být prázdné.", nameof(prijmeni));

            if (rocnik <= 0)
                throw new ArgumentException("Ročník musí být kladné číslo.", nameof(rocnik));

            Jmeno = jmeno.Trim();
            Prijmeni = prijmeni.Trim();
            Rocnik = rocnik;
        }

        public override string ToString()
            => $"{Prijmeni} {Jmeno} — {Rocnik}. ročník";
    }
}
