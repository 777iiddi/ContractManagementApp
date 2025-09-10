using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;

public class CreateTypeContratCommandHandler : IRequestHandler<CreateTypeContratCommand, int>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public CreateTypeContratCommandHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<int> Handle(CreateTypeContratCommand request, CancellationToken cancellationToken)
    {
        var typeContrat = new TypeContrat
        {
            Nom = request.Nom,
            Description = request.Description,
            DureeDefautMois = request.DureeDefautMois,
            PeriodeEssaiDefautJours = request.PeriodeEssaiDefautJours,
            PreavisDefautJours = request.PreavisDefautJours,
            ModeleDocumentId = request.ModeleDocumentId,
            EstActif = true
        };

        // Ajouter les champs obligatoires
        foreach (var champ in request.ChampsObligatoires)
        {
            typeContrat.ChampsObligatoires.Add(new ChampObligatoire
            {
                NomChamp = champ.NomChamp,
                TypeChamp = champ.TypeChamp,
                EstRequis = champ.EstRequis,
                ValeurParDefaut = champ.ValeurParDefaut,
                ValidationRegex = champ.ValidationRegex,
                MessageErreur = champ.MessageErreur,
                PaysId = champ.PaysId
            });
        }

        var result = await _typeContratRepository.AddAsync(typeContrat, cancellationToken);
        return result.Id;
    }
}
