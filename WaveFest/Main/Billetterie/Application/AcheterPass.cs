namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>Commande (au sens CQRS) : l'intention du festivalier.</summary>
public sealed record AcheterPass(FestivalierId Acheteur, Email Contact, Titulaire Titulaire, Formule Formule, Quantite Quantite);
