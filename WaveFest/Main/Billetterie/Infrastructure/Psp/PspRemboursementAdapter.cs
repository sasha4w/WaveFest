namespace WaveFest.Main.Billetterie.Infrastructure.Psp;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>ACL côté remboursement (TP4).</summary>
public class PspRemboursementAdapter : PortRemboursement
{
    private readonly ApiPspSimulee _psp;

    public PspRemboursementAdapter(ApiPspSimulee psp)
    {
        _psp = psp;
    }

    public bool Rembourser(BilletId billet, Montant montant)
    {
        return "succeeded" == _psp.CreateRefund(billet.Valeur, montant.EnCentimes()).Status;
    }
}
