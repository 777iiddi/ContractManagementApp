using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratDetail;

public class GetTypeContratDetailQueryHandler : IRequestHandler<GetTypeContratDetailQuery, TypeContratDto>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public GetTypeContratDetailQueryHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<TypeContratDto> Handle(GetTypeContratDetailQuery request, CancellationToken cancellationToken)
    {
        var typeContrat = await _typeContratRepository.GetByIdWithDetailsAsync(request.Id);

        if (typeContrat == null)
            throw new Exception("Type de contrat non trouvé");

        return new TypeContratDto
        {
            Id = typeContrat.Id,
            Nom = typeContrat.Nom,
            Description = typeContrat.Description,
            EstActif = typeContrat.EstActif,
            DureeDefautMois = typeContrat.DureeDefautMois,
            PeriodeEssaiDefautJours = typeContrat.PeriodeEssaiDefautJours,
            PreavisDefautJours = typeContrat.PreavisDefautJours,
            ModeleDocumentId = typeContrat.ModeleDocumentId,
            ModeleDocumentNom = typeContrat.ModeleDocument?.Nom,
            ChampsObligatoires = typeContrat.ChampsObligatoires.Select(co => new ChampObligatoireDto
            {
                Id = co.Id,
                NomChamp = co.NomChamp,
                TypeChamp = co.TypeChamp,
                EstRequis = co.EstRequis,
                ValeurParDefaut = co.ValeurParDefaut,
                ValidationRegex = co.ValidationRegex,
                MessageErreur = co.MessageErreur,
                PaysId = co.PaysId,
                PaysNom = co.Pays?.Nom
            }).ToList()
        };
    }
}
