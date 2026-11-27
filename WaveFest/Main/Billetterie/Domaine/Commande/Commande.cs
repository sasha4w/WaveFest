namespace WaveFest.Main.Billetterie.Domaine.Commande;

using WaveFest.Main.Billetterie.Domaine.Commun;
using WaveFest.Main.Billetterie.Domaine.Offre;

/// <summary>
/// Racine d'agrégat. Invariants protégés :
///  - R1 : 6 billets maximum par commande ;
///  - R4 : une commande non payée sous 15 minutes expire (ses places sont libérées) ;
///  - aucune modification après paiement ou expiration ;
///  - le total est toujours la somme des lignes.
/// </summary>
public class Commande : RacineAgregat
{
    public const int MaxBillets = 6;
    public static readonly TimeSpan DelaiDePaiement = TimeSpan.FromMinutes(15);

    private readonly List<LigneCommande> _lignes = [];
    private readonly List<BilletAttribue> _billets = [];

    private Commande(CommandeId id, FestivalierId acheteur, Email contact, Titulaire titulaire, DateTimeOffset ouverteLe)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Acheteur = acheteur ?? throw new ArgumentNullException(nameof(acheteur));
        Contact = contact ?? throw new ArgumentNullException(nameof(contact));
        Titulaire = titulaire ?? throw new ArgumentNullException(nameof(titulaire));
        OuverteLe = ouverteLe;
    }

    public static Commande Ouvrir(CommandeId id, FestivalierId acheteur, Email contact, Titulaire titulaire, DateTimeOffset maintenant)
    {
        return new Commande(id, acheteur, contact, titulaire, maintenant);
    }

    /// <summary>Ajoute en une seule fois toutes les tranches d'une réservation (tout ou rien).</summary>
    public void AjouterBillets(Formule formule, IReadOnlyList<Tranche> tranches)
    {
        ExigerStatut(StatutCommande.EN_COURS);
        int demandes = tranches.Sum(t => t.Quantite.Valeur);
        // TODO TP3 R1 : refuser l'ajout si la commande dépasse 6 billets (LimiteDeBilletsDepasseeException)
        foreach (Tranche t in tranches)
        {
            _lignes.Add(new LigneCommande(formule, t.Quantite, t.PrixUnitaire, t.Vague));
        }
    }

    public int NombreDeBillets() => _lignes.Sum(l => l.Quantite.Valeur);

    public Montant Total() => _lignes.Select(l => l.SousTotal()).Aggregate(Montant.ZeroEuro, (a, b) => a.Ajouter(b));

    /// <summary>R4 : vrai si la commande est encore en cours alors que le délai de paiement est écoulé.</summary>
    public bool EstExpireeA(DateTimeOffset maintenant)
    {
        // TODO TP3 R4 : en cours ET 15 minutes écoulées depuis l'ouverture
        throw new NotImplementedException("TODO TP3 R4");
    }

    /// <summary>À appeler AVANT de solliciter le PSP : on ne fait pas payer une commande expirée ou vide.</summary>
    public void VerifierPayableA(DateTimeOffset maintenant)
    {
        ExigerStatut(StatutCommande.EN_COURS);
        // TODO TP3 R4 : refuser le paiement d'une commande expirée (CommandeExpireeException)
        if (_lignes.Count == 0)
        {
            throw new ExceptionMetier("Impossible de payer une commande vide");
        }
    }

    /// <summary>Le paiement est confirmé : on attribue un billet par unité achetée et on publie CommandePayee.</summary>
    public void ConfirmerPaiement(DateTimeOffset maintenant)
    {
        VerifierPayableA(maintenant);
        foreach (LigneCommande ligne in _lignes)
        {
            for (int i = 0; i < ligne.Quantite.Valeur; i++)
            {
                _billets.Add(new BilletAttribue(BilletId.Nouveau(), ligne.Formule, ligne.PrixUnitaire));
            }
        }
        Statut = StatutCommande.PAYEE;
        Enregistrer(new CommandePayee(Id, Acheteur, Contact, Titulaire, Total(), [.. _billets], maintenant));
    }

    /// <summary>R4 : la commande expire ; l'événement porte les lignes pour que l'on puisse libérer les quotas.</summary>
    public void Expirer(DateTimeOffset maintenant)
    {
        // TODO TP3 R4 : vérifier l'expiration, passer au statut EXPIREE, enregistrer CommandeExpiree
    }

    private void ExigerStatut(StatutCommande attendu)
    {
        if (Statut != attendu)
        {
            throw new CommandeNonModifiableException(Id, Statut);
        }
    }

    public CommandeId Id { get; }

    public FestivalierId Acheteur { get; }

    public Email Contact { get; }

    public Titulaire Titulaire { get; }

    public DateTimeOffset OuverteLe { get; }

    public StatutCommande Statut { get; private set; } = StatutCommande.EN_COURS;

    /// <summary>Vue non modifiable : impossible de contourner les règles depuis l'extérieur.</summary>
    public IReadOnlyList<LigneCommande> Lignes => [.. _lignes];

    public IReadOnlyList<BilletAttribue> BilletsAttribues => [.. _billets];
}
