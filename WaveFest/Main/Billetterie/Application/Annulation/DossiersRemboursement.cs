namespace WaveFest.Main.Billetterie.Application.Annulation;

/// <summary>Port : stockage de l'état de la saga.</summary>
public interface DossiersRemboursement
{
    void Enregistrer(DossierRemboursement dossier);

    bool ExistentPour(string annulationId);

    IReadOnlyList<DossierRemboursement> DeLAnnulation(string annulationId);

    IReadOnlyList<DossierRemboursement> ATraiterManuellement();
}
