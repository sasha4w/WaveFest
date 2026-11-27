namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class CommandeIntrouvableException : ExceptionMetier
{
    public CommandeIntrouvableException(CommandeId id) : base("Commande introuvable : " + id)
    {
    }
}
