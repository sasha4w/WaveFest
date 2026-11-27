namespace WaveFest.Main.Billetterie.Application;

using WaveFest.Main.Billetterie.Application.Ports;
using WaveFest.Main.Billetterie.Domaine.Billet;
using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>R5 : changer le nom porté par un billet.</summary>
public class ChangerTitulaireUseCase
{
    private readonly Billets _billets;
    private readonly DatesFestival _dates;
    private readonly Horloge _horloge;

    public ChangerTitulaireUseCase(Billets billets, DatesFestival dates, Horloge horloge)
    {
        _billets = billets;
        _dates = dates;
        _horloge = horloge;
    }

    public void Executer(BilletId id, Titulaire nouveau)
    {
        Billet billet = _billets.ParId(id) ?? throw new ExceptionMetier("Billet introuvable : " + id);
        billet.ChangerTitulaire(nouveau, _horloge.Maintenant(), _dates);
        _billets.Enregistrer(billet);
    }
}
