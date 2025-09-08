using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Employes.Queries.GetEmployeList;

// Le Handler corrigé qui utilise la bonne méthode du repository.
public class GetEmployeListQueryHandler : IRequestHandler<GetEmployeListQuery, List<EmployeDto>>
{
    private readonly IEmployeRepository _employeRepository;

    public GetEmployeListQueryHandler(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository;
    }

    public async Task<List<EmployeDto>> Handle(GetEmployeListQuery request, CancellationToken cancellationToken)
    {
        // CORRECTION : On appelle maintenant GetAllAsync() au lieu de AddAsync().
        var employes = await _employeRepository.GetAllAsync();

        return employes.Select(e => new EmployeDto
        {
            Id = e.Id,
            Matricule = e.Matricule,
            NomComplet = $"{e.Prenom} {e.Nom}"
        }).ToList();
    }
}
