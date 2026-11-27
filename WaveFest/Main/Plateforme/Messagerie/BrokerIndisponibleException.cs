namespace WaveFest.Main.Plateforme.Messagerie;

public class BrokerIndisponibleException : Exception
{
    public BrokerIndisponibleException() : base("Broker indisponible")
    {
    }
}
