using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

// Le contrat qui définit les opérations de lecture pour les sociétés.
public interface ISocieteRepository
{
    Task<IReadOnlyList<Societe>> GetAllAsync();
    Task<Societe> AddAsync(Societe societe, CancellationToken cancellationToken);
    Task<Societe?> GetByIdAsync(int id);

}
