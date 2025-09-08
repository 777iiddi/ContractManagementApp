using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IAvenantRepository
{
    Task<IReadOnlyList<Avenant>> GetAvenantsForContratAsync(int contratId);
    Task<Avenant> AddAsync(Avenant avenant, CancellationToken cancellationToken);
}
