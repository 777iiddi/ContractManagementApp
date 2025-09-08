using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence
{
    public interface IContratRepository
    {
        Task<Contrat?> GetByIdAsync(int id);
        Task<List<Contrat>> GetAllContractsAsync();
        Task<List<Contrat>> GetContractsForValidationAsync();
        Task<List<Contrat>> GetContractsForValidationByManagerIdAsync(int managerId);
        Task<Contrat> AddAsync(Contrat contrat, CancellationToken cancellationToken);
        Task<Contrat> UpdateAsync(Contrat contrat, CancellationToken cancellationToken);
        Task UpdateAsync(Contrat contrat);
    }
}
