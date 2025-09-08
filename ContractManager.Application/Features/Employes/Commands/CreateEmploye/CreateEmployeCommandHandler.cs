using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;

namespace ContractManager.Application.Features.Employes.Commands.CreateEmploye;

// Le Handler corrigé qui utilise l'interface IEmployeRepository et non le DbContext.
public class CreateEmployeCommandHandler : IRequestHandler<CreateEmployeCommand, int>
{
    private readonly IEmployeRepository _employeRepository;

    public CreateEmployeCommandHandler(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository;
    }

    public async Task<int> Handle(CreateEmployeCommand request, CancellationToken cancellationToken)
    {
        var employe = new Employe
        {
            Matricule = request.Matricule,
            Nom = request.Nom,
            Prenom = request.Prenom
        };

        var newEmploye = await _employeRepository.AddAsync(employe);

        return newEmploye.Id;
    }
}