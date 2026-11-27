using System.Text.RegularExpressions;

namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Value Object : une adresse e-mail valide (validation volontairement simple).</summary>
public sealed record Email
{
    private static readonly Regex Format = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public string Valeur { get; }

    public Email(string valeur)
    {
        ArgumentNullException.ThrowIfNull(valeur);
        valeur = valeur.Trim().ToLowerInvariant();
        if (!Format.IsMatch(valeur))
        {
            throw new ExceptionMetier("Adresse e-mail invalide : " + valeur);
        }
        Valeur = valeur;
    }

    public static Email De(string valeur) => new(valeur);

    public override string ToString() => Valeur;
}
