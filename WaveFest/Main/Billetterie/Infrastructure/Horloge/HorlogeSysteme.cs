namespace WaveFest.Main.Billetterie.Infrastructure.Horloge;

using WaveFest.Main.Billetterie.Application.Ports;

public class HorlogeSysteme : Horloge
{
    public DateTimeOffset Maintenant() => DateTimeOffset.UtcNow;
}
