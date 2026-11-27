namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;

public sealed record CommandeExpiree(CommandeId CommandeId, IReadOnlyList<LigneCommande> Lignes, DateTimeOffset SurvenuLe) : DomainEvent;
