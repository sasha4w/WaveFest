namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Identité typée d'un festivalier (le compte client). Référence par identité entre agrégats.</summary>
public sealed record FestivalierId
{
    public string Valeur { get; }

    public FestivalierId(string valeur)
    {
        Valeur = valeur ?? throw new ArgumentNullException(nameof(valeur));
    }

    public static FestivalierId Nouveau() => new(Guid.NewGuid().ToString());

    public static FestivalierId De(string valeur) => new(valeur);

    public override string ToString() => Valeur;
}
