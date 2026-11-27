namespace WaveFest.Main.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Value Object : un billet proposé sur la bourse de revente officielle (R6).</summary>
public sealed record OffreDeRevente(BilletId Billet, Montant Prix, Montant Commission, Montant NetVendeur)
{
    public const int CommissionPct = 5;

    /// <summary>R6 : la commission de WaveFest est de 5 % du prix de revente ; le vendeur touche le reste.</summary>
    public static OffreDeRevente Pour(BilletId billet, Montant prix)
    {
        // TODO TP3 R6 : calculer la commission (5 %) et le net vendeur
        throw new NotImplementedException("TODO TP3 R6");
    }
}
