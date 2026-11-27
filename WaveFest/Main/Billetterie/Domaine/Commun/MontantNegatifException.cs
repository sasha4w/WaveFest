using System.Globalization;

namespace WaveFest.Main.Billetterie.Domaine.Commun;

public class MontantNegatifException : ExceptionMetier
{
    public MontantNegatifException(decimal valeur)
        : base("Un montant ne peut pas être négatif : " + valeur.ToString(CultureInfo.InvariantCulture))
    {
    }
}
