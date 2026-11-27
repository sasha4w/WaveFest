namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>TP4 : port de remboursement (le PSP simulé échoue 1 fois sur 5).</summary>
public interface PortRemboursement
{
    /// <returns>vrai si le remboursement est accepté par le PSP.</returns>
    bool Rembourser(BilletId billet, Montant montant);
}
