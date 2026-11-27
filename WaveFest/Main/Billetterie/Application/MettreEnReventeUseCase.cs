namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>R6 : proposer un billet sur la bourse de revente officielle.</summary>
public class MettreEnReventeUseCase
{
    private readonly Billets _billets;

    public MettreEnReventeUseCase(Billets billets)
    {
        _billets = billets;
    }

    public OffreDeRevente Executer(BilletId id, Montant prixDemande)
    {
        Billet billet = _billets.ParId(id) ?? throw new ExceptionMetier("Billet introuvable : " + id);
        OffreDeRevente offre = billet.MettreEnRevente(prixDemande);
        _billets.Enregistrer(billet);
        return offre;
    }
}
