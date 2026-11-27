namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Identité typée : impossible de confondre un identifiant de commande avec un autre.</summary>
public sealed record CommandeId
{
    public string Valeur { get; }

    public CommandeId(string valeur)
    {
        Valeur = valeur ?? throw new ArgumentNullException(nameof(valeur));
    }

    public static CommandeId Nouveau() => new(Guid.NewGuid().ToString());

    public static CommandeId De(string valeur) => new(valeur);

    public override string ToString() => Valeur;
}
