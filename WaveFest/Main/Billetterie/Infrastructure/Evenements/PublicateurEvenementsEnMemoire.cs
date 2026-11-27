namespace WaveFest.Main.Billetterie.Infrastructure.Evenements;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>TP3 : garde les événements en mémoire (pratique pour les tests).</summary>
public class PublicateurEvenementsEnMemoire : PublicateurEvenements
{
    private readonly List<DomainEvent> _publies = [];

    public void Publier(IReadOnlyList<DomainEvent> evenements)
    {
        _publies.AddRange(evenements);
    }

    public IReadOnlyList<DomainEvent> Publies => [.. _publies];
}
