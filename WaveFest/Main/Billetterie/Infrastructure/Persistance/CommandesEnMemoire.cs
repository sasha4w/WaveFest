namespace WaveFest.Main.Billetterie.Infrastructure.Persistance;

using System.Collections.Concurrent;
using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Adapter : implémentation en mémoire du port Commandes (suffisante pour le TP).</summary>
public class CommandesEnMemoire : Commandes
{
    private readonly ConcurrentDictionary<CommandeId, Commande> _stockage = new();

    public Commande? ParId(CommandeId id) => _stockage.GetValueOrDefault(id);

    public void Enregistrer(Commande commande)
    {
        _stockage[commande.Id] = commande;
    }

    public IReadOnlyList<Commande> EnCoursOuvertesAvant(DateTimeOffset limite)
    {
        return _stockage.Values
                .Where(c => c.Statut == StatutCommande.EN_COURS)
                .Where(c => c.OuverteLe <= limite)
                .ToList();
    }
}
