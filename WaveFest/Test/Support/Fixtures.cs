namespace WaveFest.Test.Support;

using System.Globalization;
using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>Données de test partagées. Le festival ouvre le vendredi 9 juillet 2027 à 16h (UTC).</summary>
public static class Fixtures
{
    public static readonly DateTimeOffset Maintenant = DateTimeOffset.Parse("2027-01-10T10:00:00Z", CultureInfo.InvariantCulture);
    public static readonly DateTimeOffset FinEarly = DateTimeOffset.Parse("2027-01-31T23:00:00Z", CultureInfo.InvariantCulture);
    public static readonly DateTimeOffset FinRegular = DateTimeOffset.Parse("2027-05-31T22:00:00Z", CultureInfo.InvariantCulture);
    public static readonly DateTimeOffset FinLastMinute = DateTimeOffset.Parse("2027-07-11T22:00:00Z", CultureInfo.InvariantCulture);
    public static readonly DatesFestival Dates = new(DateTimeOffset.Parse("2027-07-09T16:00:00Z", CultureInfo.InvariantCulture));

    public static readonly Titulaire Alice = Titulaire.De("Alice", "Martin");
    public static readonly Titulaire Bob = Titulaire.De("Bob", "Durand");
    public static readonly Email EmailAlice = Email.De("alice@example.org");

    /// <summary>EARLY : 10 places (59 / 149 / 299 €), REGULAR : 20 places (69 / 169 / 349 €), LAST_MINUTE : 1000 places.</summary>
    public static GrilleTarifaire GrilleStandard()
    {
        return new GrilleTarifaire([
            new Vague(CodeVague.EARLY, FinEarly, 10, Prix(59, 149, 299)),
            new Vague(CodeVague.REGULAR, FinRegular, 20, Prix(69, 169, 349)),
            new Vague(CodeVague.LAST_MINUTE, FinLastMinute, 1000, Prix(79, 189, 399))]);
    }

    public static IReadOnlyDictionary<TypePass, Montant> Prix(long unJour, long troisJours, long vip)
    {
        return new Dictionary<TypePass, Montant>
        {
            [TypePass.PASS_1_JOUR] = Montant.Euros(unJour),
            [TypePass.PASS_3_JOURS] = Montant.Euros(troisJours),
            [TypePass.VIP] = Montant.Euros(vip),
        };
    }

    public static Commande UneCommandeOuverteA(DateTimeOffset instant)
    {
        return Commande.Ouvrir(CommandeId.Nouveau(), FestivalierId.Nouveau(), EmailAlice, Alice, instant);
    }

    public static IReadOnlyList<Tranche> Tranche(int quantite, long prixUnitaire)
    {
        return [new Tranche(CodeVague.EARLY, Quantite.De(quantite), Montant.Euros(prixUnitaire))];
    }
}
