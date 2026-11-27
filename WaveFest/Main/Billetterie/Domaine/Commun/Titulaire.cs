namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Value Object : la personne dont le nom figure sur un billet nominatif.</summary>
public sealed record Titulaire
{
    public string Prenom { get; }
    public string Nom { get; }

    public Titulaire(string prenom, string nom)
    {
        if (string.IsNullOrWhiteSpace(prenom) || string.IsNullOrWhiteSpace(nom))
        {
            throw new ExceptionMetier("Un billet nominatif exige un prénom et un nom");
        }
        Prenom = prenom.Trim();
        Nom = nom.Trim();
    }

    public static Titulaire De(string prenom, string nom) => new(prenom, nom);

    public override string ToString() => Prenom + " " + Nom;
}
