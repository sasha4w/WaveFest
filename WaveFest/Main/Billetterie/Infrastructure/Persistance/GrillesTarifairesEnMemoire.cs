namespace WaveFest.Main.Billetterie.Infrastructure.Persistance;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Offre;

public class GrillesTarifairesEnMemoire : GrillesTarifaires
{
    private GrilleTarifaire _courante;

    public GrillesTarifairesEnMemoire(GrilleTarifaire initiale)
    {
        _courante = initiale;
    }

    public GrilleTarifaire Courante() => _courante;

    public void Enregistrer(GrilleTarifaire grille)
    {
        _courante = grille;
    }
}
