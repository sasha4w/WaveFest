namespace WaveFest.Test.Support;

using System.Globalization;
using WaveFest.Main.Billetterie.Application.Ports;

/// <summary>Horloge de test : le temps n'avance que quand on le décide.</summary>
public class HorlogeFixe : Horloge
{
    private DateTimeOffset _maintenant;

    public HorlogeFixe(DateTimeOffset depart)
    {
        _maintenant = depart;
    }

    public static HorlogeFixe Au(string iso) => new(DateTimeOffset.Parse(iso, CultureInfo.InvariantCulture));

    public DateTimeOffset Maintenant() => _maintenant;

    public void Avancer(TimeSpan duree)
    {
        _maintenant = _maintenant + duree;
    }

    public void AllerAu(string iso)
    {
        _maintenant = DateTimeOffset.Parse(iso, CultureInfo.InvariantCulture);
    }
}
