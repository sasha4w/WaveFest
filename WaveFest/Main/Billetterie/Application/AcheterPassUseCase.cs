namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>
/// Application service (EXEMPLE COMPLET) : aucun « if » métier ici.
/// Il charge, délègue au domaine, enregistre.
/// </summary>
public class AcheterPassUseCase
{
    private readonly Commandes _commandes;
    private readonly GrillesTarifaires _grilles;
    private readonly Horloge _horloge;

    public AcheterPassUseCase(Commandes commandes, GrillesTarifaires grilles, Horloge horloge)
    {
        _commandes = commandes;
        _grilles = grilles;
        _horloge = horloge;
    }

    /// <summary>Ouvre une commande et y place les premiers billets.</summary>
    public CommandeId Executer(AcheterPass demande)
    {
        DateTimeOffset maintenant = _horloge.Maintenant();
        Commande commande = Commande.Ouvrir(CommandeId.Nouveau(), demande.Acheteur, demande.Contact, demande.Titulaire, maintenant);
        Ajouter(commande, demande.Formule, demande.Quantite, maintenant);
        return commande.Id;
    }

    /// <summary>Ajoute des billets à une commande existante.</summary>
    public void AjouterBillets(CommandeId id, Formule formule, Quantite quantite)
    {
        Commande commande = _commandes.ParId(id) ?? throw new CommandeIntrouvableException(id);
        Ajouter(commande, formule, quantite, _horloge.Maintenant());
    }

    private void Ajouter(Commande commande, Formule formule, Quantite quantite, DateTimeOffset maintenant)
    {
        GrilleTarifaire grille = _grilles.Courante();
        IReadOnlyList<Tranche> tranches = grille.Reserver(formule.Type, quantite, maintenant);
        try
        {
            commande.AjouterBillets(formule, tranches);
        }
        catch (ExceptionMetier)
        {
            grille.Liberer(tranches); // on rend les places réservées
            throw;
        }
        // Compromis assumé (cf. ADR) : deux agrégats modifiés dans le même cas d'utilisation.
        _grilles.Enregistrer(grille);
        _commandes.Enregistrer(commande);
    }
}
