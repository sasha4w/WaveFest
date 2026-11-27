namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class LimiteDeBilletsDepasseeException : ExceptionMetier
{
    public LimiteDeBilletsDepasseeException(CommandeId id, int maximum)
        : base("La commande " + id + " ne peut pas dépasser " + maximum + " billets")
    {
    }
}
