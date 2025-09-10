// PaysRepository.cs
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

    public async Task<Pays?> GetByIdAsync(int id)
    {
        return await _dbContext.Pays.FindAsync(id);
    }

    public async Task<List<Pays>> GetAllAsync()
    {
        return await _dbContext.Pays
            .Where(p => p.EstActif)
            .OrderBy(p => p.Nom)
            .ToListAsync();
    }

    public async Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken)
    {
        await _dbContext.Pays.AddAsync(pays, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return pays;
    }

    public async Task<Pays> UpdateAsync(Pays pays, CancellationToken cancellationToken)
    {
        _dbContext.Entry(pays).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return pays;
    }
}
