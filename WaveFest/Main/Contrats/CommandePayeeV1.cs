namespace WaveFest.Main.Contrats;

/// <summary>
/// Événement d'intégration publié par la Billetterie (Published Language).
/// Règles : types simples, aucun type du domaine, champs ajoutés = compatibles,
/// champ supprimé ou renommé = nouvelle version (V2) publiée en parallèle.
/// </summary>
/// <param name="MontantTotalCentimes">total en centimes d'euro</param>
/// <param name="SurvenuLe">instant ISO-8601</param>
public sealed record CommandePayeeV1(
    string EventId,
    string CommandeId,
    string EmailAcheteur,
    string NomTitulaire,
    long MontantTotalCentimes,
    IReadOnlyList<CommandePayeeV1.BilletV1> Billets,
    string SurvenuLe
)
{
    /// <param name="Jour">VENDREDI, SAMEDI, DIMANCHE ou null si le pass n'est pas un pass 1 jour</param>
    public sealed record BilletV1(string BilletId, string TypePass, string? Jour);
}
