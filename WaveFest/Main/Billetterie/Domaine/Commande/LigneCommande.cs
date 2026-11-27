namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>Value Object interne à l'agrégat Commande.</summary>
public sealed record LigneCommande(Formule Formule, Quantite Quantite, Montant PrixUnitaire, CodeVague Vague)
{
    public Montant SousTotal() => PrixUnitaire.MultiplierPar(Quantite.Valeur);
}
