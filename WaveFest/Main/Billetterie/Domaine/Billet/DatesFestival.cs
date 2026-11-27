namespace WaveFest.Main.Billetterie.Domaine.Billet;

/// <summary>Value Object : début du festival (ouverture des portes le premier jour).</summary>
public sealed record DatesFestival(DateTimeOffset Ouverture)
{
    /// <summary>R5 : date limite du changement de nom gratuit (J-7).</summary>
    public DateTimeOffset LimiteChangementDeNom() => Ouverture - TimeSpan.FromDays(7);
}
