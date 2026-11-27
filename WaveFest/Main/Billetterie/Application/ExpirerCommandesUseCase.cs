namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Application.Ports;

/// <summary>
/// R4 : exécuté périodiquement (toutes les minutes en production).
/// Fait expirer les commandes non payées sous 15 minutes et rend leurs places aux quotas.
/// </summary>
public class ExpirerCommandesUseCase
{
    private readonly Commandes _commandes;
    private readonly GrillesTarifaires _grilles;
    private readonly PublicateurEvenements _publicateur;
    private readonly Horloge _horloge;

    public ExpirerCommandesUseCase(Commandes commandes, GrillesTarifaires grilles,
                                   PublicateurEvenements publicateur, Horloge horloge)
    {
        _commandes = commandes;
        _grilles = grilles;
        _publicateur = publicateur;
        _horloge = horloge;
    }

    /// <returns>le nombre de commandes expirées.</returns>
    public int Executer()
    {
        // TODO TP3 R4 : faire expirer les commandes concernées et libérer leurs places
        throw new NotImplementedException("TODO TP3 R4");
    }
}
