namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Classe de base des racines d'agrégat : elles enregistrent leurs événements du domaine.</summary>
public abstract class RacineAgregat
{
    private readonly List<DomainEvent> _evenements = [];

    protected void Enregistrer(DomainEvent evenement)
    {
        _evenements.Add(evenement);
    }

    /// <summary>Événements enregistrés depuis le dernier ExtraireEvenements().</summary>
    public IReadOnlyList<DomainEvent> Evenements => [.. _evenements];

    /// <summary>Renvoie les événements et vide la liste (appelé par l'application service).</summary>
    public IReadOnlyList<DomainEvent> ExtraireEvenements()
    {
        IReadOnlyList<DomainEvent> copie = [.. _evenements];
        _evenements.Clear();
        return copie;
    }
}
