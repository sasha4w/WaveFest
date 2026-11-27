namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Port (repository) : la collection des commandes, en langage métier.</summary>
public interface Commandes
{
    Commande? ParId(CommandeId id);

    void Enregistrer(Commande commande);

    /// <summary>Commandes encore en cours ouvertes avant cet instant (candidates à l'expiration, R4).</summary>
    IReadOnlyList<Commande> EnCoursOuvertesAvant(DateTimeOffset limite);
}
