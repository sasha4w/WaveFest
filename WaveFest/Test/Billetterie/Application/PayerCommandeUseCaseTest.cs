namespace WaveFest.Test.Billetterie.Application;

using WaveFest.Main.Billetterie.Application;
using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;
using WaveFest.Main.Billetterie.Infrastructure.Evenements;
using WaveFest.Main.Billetterie.Infrastructure.Persistance;
using WaveFest.Test.Support;
using static WaveFest.Test.Support.Fixtures;

/// <summary>Tests d'application : de vrais objets du domaine, des adapters en mémoire, un faux PSP.</summary>
public class PayerCommandeUseCaseTest
{
    private readonly HorlogeFixe _horloge = new(Fixtures.Maintenant);
    private readonly CommandesEnMemoire _commandes = new();
    private readonly BilletsEnMemoire _billets = new();
    private readonly GrillesTarifairesEnMemoire _grilles = new(Fixtures.GrilleStandard());
    private readonly PublicateurEvenementsEnMemoire _publicateur = new();
    private readonly PspQuiAccepte _pspQuiAccepte = new();

    private readonly AcheterPassUseCase _acheter;
    private readonly PayerCommandeUseCase _payer;

    public PayerCommandeUseCaseTest()
    {
        _acheter = new AcheterPassUseCase(_commandes, _grilles, _horloge);
        _payer = new PayerCommandeUseCase(_commandes, _billets, _pspQuiAccepte, _publicateur, _horloge);
    }

    private sealed class PspQuiAccepte : PortPaiement
    {
        public int AppelsPsp { get; private set; }

        public ResultatPaiement Payer(CommandeId commande, Montant montant)
        {
            AppelsPsp++;
            return new ResultatPaiement.PaiementConfirme(commande, "ref-test");
        }
    }

    private CommandeId AliceAchete(Formule formule, int quantite)
    {
        return _acheter.Executer(new AcheterPass(FestivalierId.Nouveau(), EmailAlice, Alice, formule, Quantite.De(quantite)));
    }

    [Fact(Skip = "TP3 : à activer une fois PayerCommandeUseCase implémenté")]
    public void Payer_emet_un_billet_par_place_et_publie_CommandePayee()
    {
        // TODO TP3 : Given une commande de 3 pass, When on la paie, Then 3 billets émis, commande PAYEE, CommandePayee publié
    }

    [Fact(Skip = "TP3 R4")]
    public void R4_une_commande_expiree_n_est_jamais_envoyee_au_psp()
    {
        // TODO TP3 R4 : 16 minutes après l'achat, payer lève CommandeExpireeException et le PSP n'est pas appelé
    }
}
