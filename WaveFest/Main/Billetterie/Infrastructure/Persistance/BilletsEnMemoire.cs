namespace WaveFest.Main.Billetterie.Infrastructure.Persistance;

using System.Collections.Concurrent;
using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

public class BilletsEnMemoire : Billets
{
    private readonly ConcurrentDictionary<BilletId, Billet> _stockage = new();

    public Billet? ParId(BilletId id) => _stockage.GetValueOrDefault(id);

    public void Enregistrer(Billet billet)
    {
        _stockage[billet.Id] = billet;
    }

    public IReadOnlyList<Billet> DeLaCommande(CommandeId commande)
    {
        return _stockage.Values.Where(b => b.Commande.Equals(commande)).ToList();
    }

    public IReadOnlyList<Billet> Pass1JourPour(JourFestival jour)
    {
        return _stockage.Values.Where(b => b.EstUnPass1JourPour(jour)).ToList();
    }

    public int Nombre() => _stockage.Count;
}
