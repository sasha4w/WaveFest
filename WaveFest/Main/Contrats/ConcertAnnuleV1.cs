namespace WaveFest.Main.Contrats;

/// <summary>Événement d'intégration publié par le contexte Programmation.</summary>
public sealed record ConcertAnnuleV1(string EventId, string Artiste, string Jour, string SurvenuLe);
