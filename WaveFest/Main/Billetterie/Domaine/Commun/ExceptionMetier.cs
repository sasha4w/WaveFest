namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Violation d'une règle métier. Le message est écrit en langage ubiquitaire.</summary>
public class ExceptionMetier : Exception
{
    public ExceptionMetier(string message) : base(message)
    {
    }
}
