namespace WaveFest.Main.Billetterie.Infrastructure.Persistance;

using WaveFest.Main.Billetterie.Application.Annulation;

public class DossiersRemboursementEnMemoire : DossiersRemboursement
{
    private readonly Dictionary<string, DossierRemboursement> _stockage = new();
    private readonly Lock _verrou = new();

    public void Enregistrer(DossierRemboursement dossier)
    {
        lock (_verrou)
        {
            _stockage[dossier.AnnulationId + "/" + dossier.Billet] = dossier;
        }
    }

    public bool ExistentPour(string annulationId)
    {
        lock (_verrou)
        {
            return _stockage.Values.Any(d => d.AnnulationId == annulationId);
        }
    }

    public IReadOnlyList<DossierRemboursement> DeLAnnulation(string annulationId)
    {
        lock (_verrou)
        {
            return _stockage.Values.Where(d => d.AnnulationId == annulationId).ToList();
        }
    }

    public IReadOnlyList<DossierRemboursement> ATraiterManuellement()
    {
        lock (_verrou)
        {
            return _stockage.Values.Where(d => d.Statut == StatutRemboursement.A_TRAITER_MANUELLEMENT).ToList();
        }
    }
}
