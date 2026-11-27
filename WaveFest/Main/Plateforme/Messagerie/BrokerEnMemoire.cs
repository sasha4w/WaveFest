using System.Collections.Concurrent;

namespace WaveFest.Main.Plateforme.Messagerie;

/// <summary>
/// Broker synchrone en mémoire, avec deux outils pour tester les cas d'échec :
///  - TomberEnPanne() / Redemarrer() : Publier() lève BrokerIndisponibleException ;
///  - Redistribuer(message) : simule une livraison en double (au moins une fois).
/// </summary>
public class BrokerEnMemoire : Broker
{
    private readonly ConcurrentDictionary<string, List<Action<Message>>> _abonnes = new();
    private readonly List<Message> _historique = [];
    private bool _enPanne;

    public void Publier(Message message)
    {
        if (_enPanne)
        {
            throw new BrokerIndisponibleException();
        }
        _historique.Add(message);
        Distribuer(message);
    }

    public void Abonner(string topic, Action<Message> consommateur)
    {
        _abonnes.GetOrAdd(topic, _ => []).Add(consommateur);
    }

    public void Redistribuer(Message message)
    {
        Distribuer(message);
    }

    public void TomberEnPanne()
    {
        _enPanne = true;
    }

    public void Redemarrer()
    {
        _enPanne = false;
    }

    public IReadOnlyList<Message> Historique => [.. _historique];

    private void Distribuer(Message message)
    {
        if (_abonnes.TryGetValue(message.Topic, out var consommateurs))
        {
            consommateurs.ForEach(c => c(message));
        }
    }
}
