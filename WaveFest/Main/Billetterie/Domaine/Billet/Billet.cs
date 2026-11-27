namespace WaveFest.Main.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>
/// Racine d'agrégat : un billet nominatif.
///  - R5 : changement de titulaire gratuit jusqu'à J-7, refusé ensuite ;
///  - R6 : revente au plus au prix d'achat, commission de 5 % ;
///  - (TP4) un billet n'est remboursé qu'une seule fois.
/// </summary>
public class Billet : RacineAgregat
{
    private Billet(BilletId id, CommandeId commande, Formule formule, Titulaire titulaire, Montant prixPaye)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Commande = commande ?? throw new ArgumentNullException(nameof(commande));
        Formule = formule ?? throw new ArgumentNullException(nameof(formule));
        Titulaire = titulaire ?? throw new ArgumentNullException(nameof(titulaire));
        PrixPaye = prixPaye ?? throw new ArgumentNullException(nameof(prixPaye));
    }

    public static Billet Emettre(BilletId id, CommandeId commande, Formule formule, Titulaire titulaire, Montant prixPaye)
    {
        return new Billet(id, commande, formule, titulaire, prixPaye);
    }

    /// <summary>R5 : changement de nom gratuit jusqu'à J-7 (réponse de l'expert métier : refusé ensuite).</summary>
    public void ChangerTitulaire(Titulaire nouveau, DateTimeOffset maintenant, DatesFestival dates)
    {
        ExigerStatut(StatutBillet.VALIDE);
        // TODO TP3 R5 : refuser le changement à partir de J-7 (ChangementDeNomHorsDelaiException)
        Titulaire = nouveau ?? throw new ArgumentNullException(nameof(nouveau));
    }

    /// <summary>R6 : mise en revente sur la bourse officielle, prix plafonné au prix d'achat.</summary>
    public OffreDeRevente MettreEnRevente(Montant prixDemande)
    {
        ExigerStatut(StatutBillet.VALIDE);
        // TODO TP3 R6 : refuser un prix supérieur au prix payé, créer l'offre, passer EN_REVENTE
        throw new NotImplementedException("TODO TP3 R6");
    }

    /// <summary>Un acquéreur a payé : le billet change de titulaire et de prix de référence.</summary>
    public void ConclureRevente(Titulaire acquereur, DateTimeOffset maintenant)
    {
        ExigerStatut(StatutBillet.EN_REVENTE);
        Titulaire ancien = Titulaire;
        Titulaire = acquereur ?? throw new ArgumentNullException(nameof(acquereur));
        PrixPaye = OffreEnCours!.Prix;
        Statut = StatutBillet.VALIDE;
        Enregistrer(new BilletRevendu(Id, ancien, acquereur, OffreEnCours, maintenant));
        OffreEnCours = null;
    }

    public void RetirerDeLaRevente()
    {
        ExigerStatut(StatutBillet.EN_REVENTE);
        OffreEnCours = null;
        Statut = StatutBillet.VALIDE;
    }

    /// <summary>Vrai pour un pass 1 jour du jour donné (utilisé par la saga d'annulation, TP4).</summary>
    public bool EstUnPass1JourPour(JourFestival jour)
    {
        return Formule.Type == TypePass.PASS_1_JOUR && Formule.Jour == jour;
    }

    /// <summary>TP4 : remboursement partiel suite à l'annulation d'une tête d'affiche. Une seule fois.</summary>
    public void RembourserPartiellement(Montant montant)
    {
        // TODO TP4 : refuser un second remboursement, puis mémoriser le montant remboursé
    }

    private void ExigerStatut(StatutBillet attendu)
    {
        if (Statut != attendu)
        {
            throw new ExceptionMetier("Le billet " + Id + " est " + Statut + " : opération impossible");
        }
    }

    public BilletId Id { get; }

    public CommandeId Commande { get; }

    public Formule Formule { get; }

    public Titulaire Titulaire { get; private set; }

    public Montant PrixPaye { get; private set; }

    public StatutBillet Statut { get; private set; } = StatutBillet.VALIDE;

    public OffreDeRevente? OffreEnCours { get; private set; }

    public Montant? MontantRembourse { get; private set; }
}
