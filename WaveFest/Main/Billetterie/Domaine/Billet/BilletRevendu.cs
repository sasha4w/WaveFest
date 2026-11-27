namespace WaveFest.Main.Billetterie.Domaine.Billet;

using WaveFest.Main.Billetterie.Domaine.Commun;

public sealed record BilletRevendu(BilletId BilletId, Titulaire AncienTitulaire, Titulaire NouveauTitulaire,
                                   OffreDeRevente Offre, DateTimeOffset SurvenuLe) : DomainEvent;
