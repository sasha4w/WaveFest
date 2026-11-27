namespace WaveFest.Test.Billetterie.Application;

using WaveFest.Main.Billetterie.Application;
using WaveFest.Main.Billetterie.Infrastructure.Evenements;
using WaveFest.Main.Billetterie.Infrastructure.Persistance;
using WaveFest.Test.Support;

public class ExpirerCommandesUseCaseTest
{
    private readonly HorlogeFixe _horloge;
    private readonly CommandesEnMemoire _commandes = new();
    private readonly GrillesTarifairesEnMemoire _grilles = new(Fixtures.GrilleStandard());
    private readonly AcheterPassUseCase _acheter;
    private readonly ExpirerCommandesUseCase _expirer;

    public ExpirerCommandesUseCaseTest()
    {
        _horloge = new HorlogeFixe(Fixtures.Maintenant);
        _acheter = new AcheterPassUseCase(_commandes, _grilles, _horloge);
        _expirer = new ExpirerCommandesUseCase(_commandes, _grilles, new PublicateurEvenementsEnMemoire(), _horloge);
    }

    [Fact(Skip = "TP3 R4")]
    public void R4_les_places_d_une_commande_expiree_sont_rendues_au_quota()
    {
        // TODO TP3 R4 : 4 places EARLY réservées puis 15 min écoulées : la commande expire et EARLY retrouve 10 places
    }

    [Fact(Skip = "TP3 R4")]
    public void R4_une_commande_recente_n_expire_pas()
    {
        // TODO TP3 R4 : après 10 minutes, rien n'expire
    }
}
