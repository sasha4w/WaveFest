namespace WaveFest.Test.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;
using WaveFest.Test.Support;
using static WaveFest.Test.Support.Fixtures;

public class GrilleTarifaireTest
{
    [Fact(Skip = "TP3 R2")]
    public void R2_le_prix_est_celui_de_la_vague_active()
    {
        // TODO TP3 R2 : en janvier, un pass 3 jours coûte 149 € (EARLY)
    }

    [Fact(Skip = "TP3 R2")]
    public void R2_apres_sa_date_de_fin_la_vague_suivante_s_applique()
    {
        // TODO TP3 R2 : le lendemain de FinEarly, la vague active est REGULAR
    }

    [Fact(Skip = "TP3 R3")]
    public void R3_quota_epuise_la_vente_bascule_sur_la_vague_suivante()
    {
        // TODO TP3 R3 : EARLY a 10 places ; on en vend 8 puis on en demande 4 : 2 en EARLY + 2 en REGULAR
    }

    [Fact(Skip = "TP3 R3")]
    public void R3_sans_assez_de_places_rien_n_est_reserve()
    {
        // TODO TP3 R3 : demander plus que le total disponible lève PlusDePlacesException et ne consomme rien
    }
}
