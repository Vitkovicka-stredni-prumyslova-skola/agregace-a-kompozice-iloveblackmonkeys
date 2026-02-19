namespace AgregaceAKompozice
{
    public class ZaznamDochazky
{
    public DateOnly Datum { get; }
    public bool JePritomen { get; }

    public ZaznamDochazky(DateOnly datum, bool jePritomen)
    {
        Datum = datum;
        JePritomen = jePritomen;
    }

    public override string ToString()
    {
        return $"{Datum}: {(JePritomen ? "Přítomen" : "Nepřítomen")}";
    }
}
}