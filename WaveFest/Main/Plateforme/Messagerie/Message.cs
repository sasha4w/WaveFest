namespace WaveFest.Main.Plateforme.Messagerie;

/// <summary>Une enveloppe sur le broker. L'id sert de clé d'idempotence aux consommateurs.</summary>
public sealed record Message(string Id, string Topic, object Contenu);
