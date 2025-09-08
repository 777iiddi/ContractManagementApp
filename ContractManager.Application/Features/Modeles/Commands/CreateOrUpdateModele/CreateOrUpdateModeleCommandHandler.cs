using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Modeles.Commands.CreateOrUpdateModele;

public class CreateOrUpdateModeleCommandHandler : IRequestHandler<CreateOrUpdateModeleCommand, int>
{
    private readonly IModeleDocumentRepository _modeleDocumentRepository;

    public CreateOrUpdateModeleCommandHandler(IModeleDocumentRepository modeleDocumentRepository)
    {
        _modeleDocumentRepository = modeleDocumentRepository;
    }

    public async Task<int> Handle(CreateOrUpdateModeleCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0) // Création
        {
            var modele = new ModeleDocument
            {
                Nom = request.Nom,
                ContenuTemplate = request.ContenuTemplate
            };
            var newModele = await _modeleDocumentRepository.AddAsync(modele, cancellationToken);
            return newModele.Id;
        }
        else // Mise à jour
        {
            var modeleToUpdate = await _modeleDocumentRepository.GetByIdAsync(request.Id);
            if (modeleToUpdate == null)
            {
                throw new System.Exception($"Modèle avec l'ID {request.Id} introuvable.");
            }
            modeleToUpdate.Nom = request.Nom;
            modeleToUpdate.ContenuTemplate = request.ContenuTemplate;
            await _modeleDocumentRepository.UpdateAsync(modeleToUpdate);
            return modeleToUpdate.Id;
        }
    }
}
