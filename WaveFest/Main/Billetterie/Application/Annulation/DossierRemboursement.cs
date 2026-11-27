namespace WaveFest.Main.Billetterie.Application.Annulation;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// TP4 : état de la saga pour UN billet. C'est cet état persistant qui permet
/// de reprendre le processus après une panne et de ne jamais rembourser deux fois.
/// </summary>
public class DossierRemboursement
{
    public const int MaxTentatives = 3;

    public DossierRemboursement(string annulationId, BilletId billet, Montant montant)
    {
        AnnulationId = annulationId ?? throw new ArgumentNullException(nameof(annulationId));
        Billet = billet ?? throw new ArgumentNullException(nameof(billet));
        Montant = montant ?? throw new ArgumentNullException(nameof(montant));
    }

    public void EnregistrerSucces()
    {
        Tentatives++;
        Statut = StatutRemboursement.REMBOURSE;
    }

    /// <summary>Après MaxTentatives échecs, le dossier part en traitement manuel (escalade au support).</summary>
    public void EnregistrerEchec()
    {
        // TODO TP4 : compter la tentative ; au-delà de MaxTentatives, passer A_TRAITER_MANUELLEMENT
    }

    /// <summary>Relance manuelle par le support : on repart pour un cycle de tentatives.</summary>
    public void Relancer()
    {
        if (Statut == StatutRemboursement.A_TRAITER_MANUELLEMENT)
        {
            Statut = StatutRemboursement.EN_COURS;
            Tentatives = 0;
        }
    }

    public bool EstEnCours() => Statut == StatutRemboursement.EN_COURS;

    public string AnnulationId { get; }

    public BilletId Billet { get; }

    public Montant Montant { get; }

    public StatutRemboursement Statut { get; private set; } = StatutRemboursement.EN_COURS;

    public int Tentatives { get; private set; }
}
