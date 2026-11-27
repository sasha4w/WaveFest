namespace WaveFest.Main.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class PrixDeReventeTropEleveException : ExceptionMetier
{
    public PrixDeReventeTropEleveException(BilletId id, Montant demande, Montant plafond)
        : base("Revente du billet " + id + " refusée : " + demande + " dépasse le prix d'achat " + plafond)
    {
    }
}
