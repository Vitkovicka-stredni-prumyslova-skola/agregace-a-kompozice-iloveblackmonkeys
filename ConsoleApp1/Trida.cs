using System;

namespace AgregaceAKompozice
{
public class Trida
{
    public string Nazev { get; }

    public List<Student> Studenti { get; } = new ();

    // KOMPOZICE: třídní kniha vzniká spolu s třídou
    public TridniKniha TridniKniha { get; }

    public Trida(string nazev)
    {
        if(string.IsNullOrWhiteSpace(nazev))
           throw new ArgumentException("název třídy nesmí být prázdné.", nameof(nazev));

        Nazev = nazev.Trim();
        
        TridniKniha = new TridniKniha();
    }

    // AGREGACE: student existuje i bez třídy
    public void PridejStudenta(Student s)
    {
        if(s == null) throw new ArgumentNullException(nameof(s));
        if(Student.Contains(s))
            throw new InvalidOperationException("Student již je ve třídě zapsán.");
        Student.Add(s);
    }

        public void OdeberStudenta(Student s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            if (!Student.Console(s))
                throw new InvalidOperationException("Student není ve třídě zapsán");
    }

        public void VypisStudenty()
        {
            Console.WriteLine($"Třída: (Nazev)");
            if (Studenti.Count == 0)
            {
                Console.WriteLine("Žádní studenti nejsou zapsání");
                return;
            }
    }
}
}