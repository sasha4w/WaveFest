namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class CommandeExpireeException : ExceptionMetier
{
    public CommandeExpireeException(CommandeId id)
        : base("La commande " + id + " a expiré : ses places ont été libérées")
    {
    }
}
