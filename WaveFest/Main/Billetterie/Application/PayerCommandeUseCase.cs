namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Payer une commande :
///  1. charger la commande et vérifier qu'elle est payable (R4) AVANT d'appeler le PSP ;
///  2. payer via le PortPaiement ;
///  3. si le paiement est confirmé : confirmer, émettre un Billet par billet attribué,
///     enregistrer, puis publier les événements du domaine.
/// </summary>
public class PayerCommandeUseCase
{
    private readonly Commandes _commandes;
    private readonly Billets _billets;
    private readonly PortPaiement _paiement;
    private readonly PublicateurEvenements _publicateur;
    private readonly Horloge _horloge;

    public PayerCommandeUseCase(Commandes commandes, Billets billets, PortPaiement paiement,
                                PublicateurEvenements publicateur, Horloge horloge)
    {
        _commandes = commandes;
        _billets = billets;
        _paiement = paiement;
        _publicateur = publicateur;
        _horloge = horloge;
    }

    public ResultatPaiement Executer(CommandeId id)
    {
        // TODO TP3 : implémenter le cas d'utilisation (voir la documentation de la classe)
        throw new NotImplementedException("TODO TP3");
    }
}
