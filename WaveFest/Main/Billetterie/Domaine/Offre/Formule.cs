namespace WaveFest.Main.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Value Object : ce que le festivalier achète.
/// Un pass 1 jour porte obligatoirement son jour ; les autres pass n'en ont pas.
/// </summary>
public sealed record Formule
{
    public TypePass Type { get; }
    public JourFestival? Jour { get; }

    public Formule(TypePass type, JourFestival? jour)
    {
        if (type == TypePass.PASS_1_JOUR && jour == null)
        {
            throw new ExceptionMetier("Un pass 1 jour doit préciser son jour");
        }
        if (type != TypePass.PASS_1_JOUR && jour != null)
        {
            throw new ExceptionMetier("Seul un pass 1 jour précise un jour");
        }
        Type = type;
        Jour = jour;
    }

    public static Formule UnJour(JourFestival jour) => new(TypePass.PASS_1_JOUR, jour);

    public static Formule TroisJours() => new(TypePass.PASS_3_JOURS, null);

    public static Formule Vip() => new(TypePass.VIP, null);

    public JourFestival? JourConcerne() => Jour;

    public override string ToString() => Jour == null ? Type.ToString() : Type + "(" + Jour + ")";
}
