namespace WaveFest.Main.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Commun;

public class ChangementDeNomHorsDelaiException : ExceptionMetier
{
    public ChangementDeNomHorsDelaiException(BilletId id)
        : base("Le billet " + id + " ne peut plus changer de titulaire : délai de J-7 dépassé")
    {
    }
}
