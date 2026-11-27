namespace WaveFest.Main.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Une partie d'une réservation, vendue au prix d'une vague donnée.</summary>
public sealed record Tranche(CodeVague Vague, Quantite Quantite, Montant PrixUnitaire);
