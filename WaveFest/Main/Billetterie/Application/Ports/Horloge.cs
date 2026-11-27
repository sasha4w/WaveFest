namespace WaveFest.Main.Billetterie.Application.Ports;

/// <summary>Port : le temps est une dépendance comme une autre (indispensable pour tester R4 et R5).</summary>
public interface Horloge
{
    DateTimeOffset Maintenant();
}
