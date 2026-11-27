namespace WaveFest.Main.Billetterie.Infrastructure.Psp;

/// <summary>
/// Simulation de l'API du prestataire de paiement (PSP). NE PAS MODIFIER :
/// c'est un système externe, son modèle (centimes, statuts en anglais, metadata)
/// nous est imposé. C'est précisément pour cela qu'on écrit une ACL.
/// </summary>
public class ApiPspSimulee
{
    public sealed record Charge(string Id, long Amount, string Currency, string Status, IReadOnlyDictionary<string, string> Metadata);

    public sealed record Refund(string Id, string Reference, long Amount, string Status);

    private readonly Random _aleatoire;
    private readonly double _tauxEchecPaiement;
    private readonly double _tauxEchecRemboursement;

    /// <summary>Paiements toujours acceptés, remboursements en échec 1 fois sur 5 (comme demandé au TP4).</summary>
    public ApiPspSimulee(Random aleatoire) : this(aleatoire, 0.0, 0.2)
    {
    }

    public ApiPspSimulee(Random aleatoire, double tauxEchecPaiement, double tauxEchecRemboursement)
    {
        _aleatoire = aleatoire;
        _tauxEchecPaiement = tauxEchecPaiement;
        _tauxEchecRemboursement = tauxEchecRemboursement;
    }

    public Charge CreateCharge(long amountInCents, string currency, IReadOnlyDictionary<string, string> metadata)
    {
        string status = _aleatoire.NextDouble() < _tauxEchecPaiement ? "card_declined" : "succeeded";
        return new Charge("ch_" + Guid.NewGuid().ToString()[..8], amountInCents, currency, status, new Dictionary<string, string>(metadata));
    }

    public Refund CreateRefund(string reference, long amountInCents)
    {
        string status = _aleatoire.NextDouble() < _tauxEchecRemboursement ? "failed" : "succeeded";
        return new Refund("re_" + Guid.NewGuid().ToString()[..8], reference, amountInCents, status);
    }
}
