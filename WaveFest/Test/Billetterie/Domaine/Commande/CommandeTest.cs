namespace WaveFest.Test.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commande;
using WaveFest.Main.Billetterie.Domaine.Offre;
using static WaveFest.Test.Support.Fixtures;

public class CommandeTest
{
    // ---------- R1 : 6 billets maximum par commande ----------

    [Fact(Skip = "TP3 R1 : exemple complet, à activer une fois R1 implémentée")]
    public void R1_refuse_plus_de_6_billets_par_commande()
    {
        // Given
        Commande commande = UneCommandeOuverteA(Maintenant);
        commande.AjouterBillets(Formule.TroisJours(), Tranche(4, 149));
        // When / Then
        Assert.ThrowsAny<LimiteDeBilletsDepasseeException>(() => commande.AjouterBillets(Formule.Vip(), Tranche(3, 299)));
        Assert.Equal(4, commande.NombreDeBillets());
    }

    [Fact(Skip = "TP3 R1")]
    public void R1_accepte_exactement_6_billets()
    {
        // TODO TP3 R1 : Given une commande vide, When on ajoute 6 billets, Then elle contient 6 billets
    }

    [Fact]
    public void Le_total_est_la_somme_des_lignes()
    {
        Commande commande = UneCommandeOuverteA(Maintenant);
        commande.AjouterBillets(Formule.TroisJours(), Tranche(2, 149));
        commande.AjouterBillets(Formule.UnJour(JourFestival.VENDREDI), Tranche(1, 59));
        Assert.Equal("357.00 EUR", commande.Total().ToString());
    }

    // ---------- R4 : 15 minutes pour payer ----------

    [Fact(Skip = "TP3 R4")]
    public void R4_une_commande_non_payee_expire_apres_15_minutes()
    {
        // TODO TP3 R4 : pas expirée à 14 min 59 s, expirée à 15 min
    }

    [Fact(Skip = "TP3 R4")]
    public void R4_on_ne_peut_pas_payer_une_commande_expiree()
    {
        // TODO TP3 R4 : ConfirmerPaiement 16 minutes après l'ouverture lève CommandeExpireeException
    }

    [Fact(Skip = "TP3 R4")]
    public void R4_l_expiration_publie_CommandeExpiree()
    {
        // TODO TP3 R4 : Expirer() passe la commande EXPIREE et enregistre CommandeExpiree
    }

    // ---------- Paiement ----------

    [Fact]
    public void Le_paiement_attribue_un_billet_par_unite_et_publie_CommandePayee()
    {
        Commande commande = UneCommandeOuverteA(Maintenant);
        commande.AjouterBillets(Formule.TroisJours(), Tranche(3, 149));
        commande.ConfirmerPaiement(Maintenant + TimeSpan.FromMinutes(5));
        Assert.Equal(StatutCommande.PAYEE, commande.Statut);
        Assert.Equal(3, commande.BilletsAttribues.Count);
        Assert.All(commande.Evenements, e => Assert.IsType<CommandePayee>(e));
    }

    [Fact]
    public void Une_commande_payee_n_est_plus_modifiable()
    {
        Commande commande = UneCommandeOuverteA(Maintenant);
        commande.AjouterBillets(Formule.Vip(), Tranche(1, 299));
        commande.ConfirmerPaiement(Maintenant + TimeSpan.FromMinutes(1));
        Assert.ThrowsAny<CommandeNonModifiableException>(() => commande.AjouterBillets(Formule.Vip(), Tranche(1, 299)));
    }
}
