namespace WaveFest.Test.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;
using static WaveFest.Test.Support.Fixtures;

public class BilletTest
{
    private Billet UnPass3JoursAchete149Euros()
    {
        return Billet.Emettre(BilletId.Nouveau(), CommandeId.Nouveau(), Formule.TroisJours(), Alice, Montant.Euros(149));
    }

    // ---------- R5 : changement de nom gratuit jusqu'à J-7 ----------

    [Fact(Skip = "TP3 R5")]
    public void R5_changement_de_nom_accepte_avant_J_moins_7()
    {
        // TODO TP3 R5 : à J-8, le titulaire devient BOB
    }

    [Fact(Skip = "TP3 R5")]
    public void R5_changement_de_nom_refuse_a_partir_de_J_moins_7()
    {
        // TODO TP3 R5 : à J-7 pile, ChangementDeNomHorsDelaiException et le titulaire reste ALICE
    }

    // ---------- R6 : revente plafonnée, commission 5 % ----------

    [Fact(Skip = "TP3 R6")]
    public void R6_revente_refusee_au_dessus_du_prix_d_achat()
    {
        // TODO TP3 R6 : revendre à 150 € un billet payé 149 € lève PrixDeReventeTropEleveException
    }

    [Fact(Skip = "TP3 R6")]
    public void R6_revente_au_prix_d_achat_commission_de_5_pourcents()
    {
        // TODO TP3 R6 : à 149 €, commission 7,45 €, net vendeur 141,55 €, billet EN_REVENTE
    }

    [Fact(Skip = "TP3 R6")]
    public void R6_apres_revente_le_billet_change_de_titulaire()
    {
        // TODO TP3 R6 : ConclureRevente(BOB) : titulaire BOB, prix de référence = prix de revente, événement BilletRevendu
    }
}
