namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Résultat d'un paiement, dans NOTRE langage (et non celui du PSP).</summary>
public abstract record ResultatPaiement(CommandeId Commande)
{
    public sealed record PaiementConfirme(CommandeId Commande, string ReferenceExterne) : ResultatPaiement(Commande);

    public sealed record PaiementRefuse(CommandeId Commande, string Motif) : ResultatPaiement(Commande);
}
