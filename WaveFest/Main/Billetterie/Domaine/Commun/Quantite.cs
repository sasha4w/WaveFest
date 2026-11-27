namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Value Object : un nombre de billets, toujours strictement positif.</summary>
public sealed record Quantite
{
    public int Valeur { get; }

    public Quantite(int valeur)
    {
        // TODO TP3 : refuser une quantité nulle ou négative (ExceptionMetier)
        Valeur = valeur;
    }

    public static Quantite De(int valeur) => new(valeur);
}
