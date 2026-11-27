namespace WaveFest.Main.Billetterie.Infrastructure.Outbox;

using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Contrats;

/// <summary>Domain event (riche, interne) vers integration event (stable, types simples, versionné).</summary>
public static class TraducteurEvenements
{
    public static CommandePayeeV1 VersV1(string eventId, CommandePayee e)
    {
        // TODO TP4 : construire le CommandePayeeV1 (types simples uniquement : string, long...)
        throw new NotImplementedException("TODO TP4");
    }
}
