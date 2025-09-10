using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface ISocieteRepository
{
    Task<Societe?> GetByIdAsync(int id);
    Task<List<Societe>> GetAllAsync();
    Task<Societe> AddAsync(Societe societe, CancellationToken cancellationToken);
    Task<Societe> UpdateAsync(Societe societe, CancellationToken cancellationToken);
    Task DeleteAsync(int id);
}
