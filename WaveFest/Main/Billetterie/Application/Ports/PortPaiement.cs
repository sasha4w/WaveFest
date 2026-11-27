namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Port défini par la Billetterie. L'adapter PSP (l'ACL) l'implémente.</summary>
public interface PortPaiement
{
    ResultatPaiement Payer(CommandeId commande, Montant montant);
}
