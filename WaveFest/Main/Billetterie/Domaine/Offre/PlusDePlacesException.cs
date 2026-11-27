namespace WaveFest.Main.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class PlusDePlacesException : ExceptionMetier
{
    public PlusDePlacesException(int demandees, int disponibles)
        : base("Plus assez de places : " + demandees + " demandées, " + disponibles + " disponibles")
    {
    }
}
