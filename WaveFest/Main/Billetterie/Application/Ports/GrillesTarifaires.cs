namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Offre;

public interface GrillesTarifaires
{
    GrilleTarifaire Courante();

    void Enregistrer(GrilleTarifaire grille);
}
