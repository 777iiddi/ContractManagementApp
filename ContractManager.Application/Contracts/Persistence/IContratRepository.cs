using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IContratRepository
{
    // AJOUT : Méthode pour récupérer les contrats en attente de validation.
    Task<IReadOnlyList<Contrat>> GetContractsForValidationAsync();

    Task<Contrat?> GetByIdAsync(int id);
    Task UpdateAsync(Contrat contrat);
    Task<IReadOnlyList<Contrat>> GetAllContractsAsync();
    Task<Contrat> AddAsync(Contrat contrat, CancellationToken cancellationToken);
    Task<Contrat> UpdateAsync(Contrat contrat, CancellationToken cancellationToken);
    Task<IReadOnlyList<Contrat>> GetContractsForValidationByManagerIdAsync(int managerId);
}
