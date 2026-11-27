namespace WaveFest.Main.Billetterie.Infrastructure.Outbox;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Plateforme.Messagerie;

/// <summary>
/// TP4 : le relais lit l'outbox et publie sur le broker (en production : toutes les secondes).
/// Garantie « au moins une fois » : si le broker tombe, le message reste en attente et sera
/// republié au prochain passage. Les consommateurs doivent donc être idempotents.
/// </summary>
public class RelaisOutbox
{
    private readonly Outbox _outbox;
    private readonly Broker _broker;
    private readonly Horloge _horloge;

    public RelaisOutbox(Outbox outbox, Broker broker, Horloge horloge)
    {
        _outbox = outbox;
        _broker = broker;
        _horloge = horloge;
    }

    /// <returns>le nombre de messages publiés lors de ce passage.</returns>
    public int PublierEnAttente()
    {
        // TODO TP4 : publier chaque message en attente puis le marquer publié ; s'arrêter si le broker est indisponible
        throw new NotImplementedException("TODO TP4");
    }
}
