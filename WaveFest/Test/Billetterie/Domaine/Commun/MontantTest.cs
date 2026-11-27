namespace WaveFest.Test.Billetterie.Domaine.Commun;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>EXEMPLE COMPLET : des tests de Value Object, nommés comme des règles.</summary>
public class MontantTest
{
    [Fact]
    public void Un_montant_ne_peut_pas_etre_negatif()
    {
        Assert.ThrowsAny<MontantNegatifException>(() => Montant.Euros("-1"));
    }

    [Fact]
    public void Deux_montants_de_meme_valeur_sont_egaux()
    {
        Assert.Equal(Montant.Euros("10.00"), Montant.Euros("10"));
    }

    [Fact]
    public void Ajouter_renvoie_un_nouveau_montant()
    {
        Montant dix = Montant.Euros(10);
        Montant total = dix.Ajouter(Montant.Euros("2.50"));
        Assert.Equal(Montant.Euros("12.50"), total);
        Assert.Equal(Montant.Euros(10), dix);
    }

    [Fact]
    public void Cinq_pourcents_de_149_euros_font_7_euros_45()
    {
        Assert.Equal(Montant.Euros("7.45"), Montant.Euros(149).Pourcentage(5));
    }

    [Fact]
    public void Le_psp_parle_en_centimes()
    {
        Assert.Equal(15990L, Montant.Euros("159.90").EnCentimes());
    }
}
