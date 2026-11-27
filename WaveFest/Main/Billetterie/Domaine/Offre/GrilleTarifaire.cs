namespace WaveFest.Main.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Agrégat : les vagues de vente du festival et leurs quotas.
/// R2 : le prix dépend de la vague active.
/// R3 : quand le quota d'une vague est épuisé, la vente bascule sur la vague suivante.
/// </summary>
public class GrilleTarifaire
{
    private readonly List<Vague> _vagues;

    public GrilleTarifaire(IReadOnlyList<Vague> vagues)
    {
        if (vagues.Count == 0)
        {
            throw new ExceptionMetier("Une grille tarifaire contient au moins une vague");
        }
        _vagues = vagues.OrderBy(v => v.Code).ToList();
    }

    /// <summary>La première vague ouverte à cet instant, s'il y en a une.</summary>
    public Vague? VagueActive(DateTimeOffset maintenant)
    {
        // TODO TP3 R2 : renvoyer la première vague ouverte à cet instant
        throw new NotImplementedException("TODO TP3 R2");
    }

    /// <summary>
    /// Réserve des places et renvoie les tranches tarifaires correspondantes.
    /// Si la vague active n'a plus assez de places, le reste bascule sur la vague suivante (R3).
    /// Tout ou rien : si le festival n'a pas assez de places, rien n'est réservé.
    /// </summary>
    public IReadOnlyList<Tranche> Reserver(TypePass type, Quantite quantite, DateTimeOffset maintenant)
    {
        // TODO TP3 R2/R3 : découper la quantité en tranches, vague par vague, puis consommer les quotas
        throw new NotImplementedException("TODO TP3 R2/R3");
    }

    /// <summary>Remet des places dans le quota de leur vague (commande expirée, annulation...).</summary>
    public void Liberer(IReadOnlyList<Tranche> tranches)
    {
        foreach (Tranche t in tranches)
        {
            Vague(t.Vague).Liberer(t.Quantite.Valeur);
        }
    }

    public void Liberer(CodeVague code, Quantite quantite)
    {
        Vague(code).Liberer(quantite.Valeur);
    }

    public Vague Vague(CodeVague code)
    {
        return _vagues.FirstOrDefault(v => v.Code == code)
               ?? throw new ExceptionMetier("Vague inconnue : " + code);
    }

    public IReadOnlyList<Vague> Vagues => [.. _vagues];
}
