namespace WaveFest.Main.Billetterie.Infrastructure.Outbox;

/// <summary>La table outbox, en mémoire.</summary>
public class Outbox
{
    private readonly List<MessageOutbox> _messages = [];
    private readonly Lock _verrou = new();

    public void Ajouter(MessageOutbox message)
    {
        lock (_verrou)
        {
            _messages.Add(message);
        }
    }

    /// <summary>Messages non encore publiés, dans l'ordre d'écriture.</summary>
    public IReadOnlyList<MessageOutbox> EnAttente()
    {
        lock (_verrou)
        {
            return _messages.Where(m => !m.EstPublie()).ToList();
        }
    }

    public void MarquerPublie(MessageOutbox message, DateTimeOffset instant)
    {
        lock (_verrou)
        {
            message.MarquerPublie(instant);
        }
    }

    public IReadOnlyList<MessageOutbox> Tous()
    {
        lock (_verrou)
        {
            return [.. _messages];
        }
    }
}
