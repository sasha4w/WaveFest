namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Événement du domaine (interne à la Billetterie).</summary>
public sealed record CommandePayee(
    CommandeId CommandeId,
    FestivalierId Acheteur,
    Email Contact,
    Titulaire Titulaire,
    Montant Total,
    IReadOnlyList<BilletAttribue> Billets,
    DateTimeOffset SurvenuLe
) : DomainEvent;
