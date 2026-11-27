namespace WaveFest.Main.Billetterie.Domaine.Offre;

using WaveFest.Main.Billetterie.Domaine.Commun;

/// <summary>
/// Entité interne à l'agrégat GrilleTarifaire.
/// Une vague se clôt à sa date de fin OU quand son quota est épuisé.
/// </summary>
public class Vague
{
    private readonly Dictionary<TypePass, Montant> _prix;
    private int _vendus;

    public Vague(CodeVague code, DateTimeOffset finDeVente, int quota, IReadOnlyDictionary<TypePass, Montant> prix)
    {
        Code = code;
        FinDeVente = finDeVente;
        if (quota <= 0)
        {
            throw new ExceptionMetier("Le quota d'une vague doit être positif");
        }
        Quota = quota;
        _prix = new Dictionary<TypePass, Montant>(prix);
        if (_prix.Count != Enum.GetValues<TypePass>().Length)
        {
            throw new ExceptionMetier("Une vague doit fixer un prix pour chaque type de pass");
        }
    }

    public bool EstOuverteA(DateTimeOffset maintenant)
    {
        // TODO TP3 R2/R3 : ouverte si la date de fin n'est pas passée ET s'il reste des places
        throw new NotImplementedException("TODO TP3 R2/R3");
    }

    public int PlacesRestantes() => Quota - _vendus;

    public Montant Prix(TypePass type) => _prix[type];

    internal void Vendre(int nombre)
    {
        if (nombre > PlacesRestantes())
        {
            throw new PlusDePlacesException(nombre, PlacesRestantes());
        }
        _vendus += nombre;
    }

    internal void Liberer(int nombre)
    {
        _vendus = Math.Max(0, _vendus - nombre);
    }

    public CodeVague Code { get; }

    public DateTimeOffset FinDeVente { get; }

    public int Quota { get; }
}
