namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>Un billet attribué au moment du paiement : 1 unité d'une ligne = 1 billet.</summary>
public sealed record BilletAttribue(BilletId BilletId, Formule Formule, Montant PrixPaye);
