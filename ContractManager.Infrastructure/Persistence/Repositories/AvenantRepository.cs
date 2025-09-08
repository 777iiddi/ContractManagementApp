using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class AvenantRepository : IAvenantRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public AvenantRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Avenant>> GetAvenantsForContratAsync(int contratId)
    {
        return await _dbContext.Avenants
            .Where(a => a.ContratId == contratId)
            .ToListAsync();
    }

    public async Task<Avenant> AddAsync(Avenant avenant, CancellationToken cancellationToken)
    {
        await _dbContext.Avenants.AddAsync(avenant, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return avenant;
    }
}
