using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IPaysRepository
{
    Task<IReadOnlyList<Pays>> GetAllAsync();
    // CORRECTION : On ajoute le CancellationToken à la signature de la méthode.
    Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken);
}
