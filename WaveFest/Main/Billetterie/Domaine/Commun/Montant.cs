using System.Globalization;

namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Value Object : une somme d'argent dans une devise.
/// Immuable, validé à la construction, égalité par valeur (record).
/// EXEMPLE COMPLET fourni : inspirez-vous-en pour Quantite, Email, etc.
/// </summary>
public sealed record Montant
{
    public const string Eur = "EUR";
    public static readonly Montant ZeroEuro = Euros("0");

    public decimal Valeur { get; }
    public string Devise { get; }

    public Montant(decimal valeur, string devise)
    {
        ArgumentNullException.ThrowIfNull(devise);
        if (valeur < 0)
        {
            throw new MontantNegatifException(valeur);
        }
        Valeur = Math.Round(valeur, 2, MidpointRounding.ToEven);
        Devise = devise;
    }

    public static Montant Euros(string valeur) => new(decimal.Parse(valeur, CultureInfo.InvariantCulture), Eur);

    public static Montant Euros(long valeur) => new(valeur, Eur);

    public Montant Ajouter(Montant autre)
    {
        ExigerMemeDevise(autre);
        return new Montant(Valeur + autre.Valeur, Devise);
    }

    public Montant Soustraire(Montant autre)
    {
        ExigerMemeDevise(autre);
        return new Montant(Valeur - autre.Valeur, Devise);
    }

    public Montant MultiplierPar(int facteur) => new(Valeur * facteur, Devise);

    /// <summary>Ex. : commission de 5 % : montant.Pourcentage(5).</summary>
    public Montant Pourcentage(int pct)
    {
        decimal resultat = Math.Round(Valeur * pct / 100m, 2, MidpointRounding.ToEven);
        return new Montant(resultat, Devise);
    }

    public bool EstSuperieurA(Montant autre)
    {
        ExigerMemeDevise(autre);
        return Valeur > autre.Valeur;
    }

    /// <summary>Pour les systèmes externes (PSP) qui parlent en centimes.</summary>
    public long EnCentimes() => checked((long)(Valeur * 100m));

    private void ExigerMemeDevise(Montant autre)
    {
        if (Devise != autre.Devise)
        {
            throw new ExceptionMetier("Devises différentes : " + Devise + " et " + autre.Devise);
        }
    }

    public override string ToString() => Valeur.ToString("0.00", CultureInfo.InvariantCulture) + " " + Devise;
}
