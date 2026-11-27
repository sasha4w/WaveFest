namespace WaveFest.Test.Billetterie.Infrastructure.Psp;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Infrastructure.Psp;

public class PspPaiementAdapterTest
{
    [Fact(Skip = "TP3 ACL")]
    public void Un_paiement_succeeded_devient_PaiementConfirme()
    {
        // TODO TP3 ACL : avec un PSP qui accepte tout, le résultat est un PaiementConfirme
    }

    [Fact(Skip = "TP3 ACL")]
    public void Un_paiement_refuse_par_la_banque_devient_PaiementRefuse()
    {
        // TODO TP3 ACL : avec un PSP qui refuse tout (taux d'échec 1.0), le résultat est un PaiementRefuse
    }
}
