using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class PaysRepository : IPaysRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public PaysRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Pays>> GetAllAsync()
    {
        return await _dbContext.Pays.ToListAsync();
    }

    // CORRECTION : On utilise le CancellationToken dans les appels à Entity Framework.
    public async Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken)
    {
        await _dbContext.Pays.AddAsync(pays, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return pays;
    }
}