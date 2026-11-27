namespace WaveFest.Main.Billetterie.Application.Annulation;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Offre;
using WaveFest.Main.Contrats;

/// <summary>
/// TP4 - Saga « annulation d'une tête d'affiche » (règle 9 du brief) :
/// les pass 1 jour du jour concerné sont remboursés à 50 %.
///
/// Exigences :
///  - idempotente : recevoir deux fois le même ConcertAnnuleV1 ne rembourse pas deux fois ;
///  - le PSP échoue parfois : jusqu'à MaxTentatives essais par billet ;
///  - au-delà, le dossier passe A_TRAITER_MANUELLEMENT (pas d'exception, le processus continue
///    pour les autres billets).
/// </summary>
public class SagaAnnulationConcert
{
    public const int TauxRemboursementPct = 50;

    private readonly Billets _billets;
    private readonly DossiersRemboursement _dossiers;
    private readonly PortRemboursement _remboursement;

    public SagaAnnulationConcert(Billets billets, DossiersRemboursement dossiers, PortRemboursement remboursement)
    {
        _billets = billets;
        _dossiers = dossiers;
        _remboursement = remboursement;
    }

    public void QuandConcertAnnule(ConcertAnnuleV1 evenement)
    {
        // TODO TP4 : idempotence sur evenement.EventId, un dossier par pass 1 jour concerné, puis traiter chaque dossier
    }

    /// <summary>Relance les dossiers en échec (déclenché par le support).</summary>
    public void RelancerDossiersEnEchec()
    {
        foreach (DossierRemboursement dossier in _dossiers.ATraiterManuellement())
        {
            dossier.Relancer();
            Traiter(dossier);
        }
    }

    private void Traiter(DossierRemboursement dossier)
    {
        // TODO TP4 : tant que le dossier est en cours, appeler le PSP ; succès = rembourser le billet ; échec = EnregistrerEchec()
    }
}
