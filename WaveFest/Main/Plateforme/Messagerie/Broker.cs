namespace WaveFest.Main.Plateforme.Messagerie;

/// <summary>Port technique du broker de messages (Kafka, RabbitMQ... simulé en mémoire ici).</summary>
public interface Broker
{
    void Publier(Message message);

    void Abonner(string topic, Action<Message> consommateur);
}
