namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Identité typée d'un billet.</summary>
public sealed record BilletId
{
    public string Valeur { get; }

    public BilletId(string valeur)
    {
        Valeur = valeur ?? throw new ArgumentNullException(nameof(valeur));
    }

    public static BilletId Nouveau() => new(Guid.NewGuid().ToString());

    public static BilletId De(string valeur) => new(valeur);

    public override string ToString() => Valeur;
}
