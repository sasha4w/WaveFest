namespace WaveFest.Main.Billetterie.Infrastructure.Messagerie;

using WaveFest.Main.Billetterie.Application.Annulation;
using WaveFest.Main.Contrats;
using WaveFest.Main.Plateforme.Messagerie;

/// <summary>Branche la saga de la Billetterie sur le topic publié par la Programmation.</summary>
public class ConsommateurConcertAnnule
{
    private readonly SagaAnnulationConcert _saga;

    public ConsommateurConcertAnnule(SagaAnnulationConcert saga)
    {
        _saga = saga;
    }

    public void Accept(Message message)
    {
        _saga.QuandConcertAnnule((ConcertAnnuleV1)message.Contenu);
    }
}
