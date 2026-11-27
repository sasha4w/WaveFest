namespace WaveFest.Main.Billetterie.Application.Ports;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Port : que faire des événements du domaine une fois l'agrégat enregistré ?
/// TP3 : une implémentation en mémoire suffit. TP4 : on les écrit dans l'outbox.
/// </summary>
public interface PublicateurEvenements
{
    void Publier(IReadOnlyList<DomainEvent> evenements);
}
