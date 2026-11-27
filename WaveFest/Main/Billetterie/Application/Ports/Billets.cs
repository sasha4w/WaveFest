namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

public interface Billets
{
    Billet? ParId(BilletId id);

    void Enregistrer(Billet billet);

    IReadOnlyList<Billet> DeLaCommande(CommandeId commande);

    /// <summary>TP4 : les pass 1 jour du jour donné (saga d'annulation de concert).</summary>
    IReadOnlyList<Billet> Pass1JourPour(JourFestival jour);
}
