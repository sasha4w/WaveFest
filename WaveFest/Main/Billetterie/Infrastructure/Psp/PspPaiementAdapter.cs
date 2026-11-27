namespace WaveFest.Main.Billetterie.Infrastructure.Psp;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Anticorruption Layer : traduit le modèle du PSP vers notre langage.
/// Aucun « charge », « succeeded » ou centime ne doit sortir de cette classe.
/// </summary>
public class PspPaiementAdapter : PortPaiement
{
    private readonly ApiPspSimulee _psp;

    public PspPaiementAdapter(ApiPspSimulee psp)
    {
        _psp = psp;
    }

    public ResultatPaiement Payer(CommandeId commande, Montant montant)
    {
        // TODO TP3 ACL : appeler _psp.CreateCharge (centimes, devise en minuscules, metadata ref) et traduire le statut
        throw new NotImplementedException("TODO TP3 ACL");
    }
}
