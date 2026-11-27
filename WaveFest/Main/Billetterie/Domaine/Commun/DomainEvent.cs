namespace WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>Un fait métier passé, immuable, nommé au passé.</summary>
public interface DomainEvent
{
    DateTimeOffset SurvenuLe { get; }
}
