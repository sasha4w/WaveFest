namespace WaveFest.Main.Billetterie.Infrastructure.Outbox;

/// <summary>Une ligne de la table outbox : écrite dans la MÊME transaction que l'agrégat.</summary>
public class MessageOutbox
{
    public MessageOutbox(string id, string topic, object contenu, DateTimeOffset creeLe)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        Contenu = contenu ?? throw new ArgumentNullException(nameof(contenu));
        CreeLe = creeLe;
    }

    internal void MarquerPublie(DateTimeOffset instant)
    {
        PublieLe = instant;
    }

    public bool EstPublie() => PublieLe != null;

    public string Id { get; }

    public string Topic { get; }

    public object Contenu { get; }

    public DateTimeOffset CreeLe { get; }

    public DateTimeOffset? PublieLe { get; private set; }
}
