namespace WaveFest.Main.Billetterie.Infrastructure.Outbox;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Contrats;

/// <summary>
/// TP4 : traduit les événements du domaine utiles aux autres contextes en événements
/// d'intégration (Published Language) et les écrit dans l'outbox.
/// Les événements purement internes (CommandeExpiree...) ne sortent pas du contexte.
/// </summary>
public class PublicateurOutbox : PublicateurEvenements
{
    private readonly Outbox _outbox;
    private readonly Horloge _horloge;

    public PublicateurOutbox(Outbox outbox, Horloge horloge)
    {
        _outbox = outbox;
        _horloge = horloge;
    }

    public void Publier(IReadOnlyList<DomainEvent> evenements)
    {
        // TODO TP4 : pour chaque CommandePayee, écrire un MessageOutbox (topic Topics.CommandePayeeV1, contenu CommandePayeeV1)
    }
}
