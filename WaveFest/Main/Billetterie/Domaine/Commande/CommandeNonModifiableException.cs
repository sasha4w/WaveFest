namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class CommandeNonModifiableException : ExceptionMetier
{
    public CommandeNonModifiableException(CommandeId id, StatutCommande statut)
        : base("La commande " + id + " est " + statut + " : opération impossible")
    {
    }
}
