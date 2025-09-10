using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class SocieteRepository : ISocieteRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public SocieteRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Societe?> GetByIdAsync(int id)
    {
        return await _dbContext.Societes
            .Include(s => s.Pays)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Societe>> GetAllAsync()
    {
        return await _dbContext.Societes
            .Include(s => s.Pays)
            .OrderBy(s => s.Nom)
            .ToListAsync();
    }

    public async Task<Societe> AddAsync(Societe societe, CancellationToken cancellationToken)
    {
        await _dbContext.Societes.AddAsync(societe, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return societe;
    }

    public async Task<Societe> UpdateAsync(Societe societe, CancellationToken cancellationToken)
    {
        _dbContext.Entry(societe).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return societe;
    }

    public async Task DeleteAsync(int id)
    {
        var societe = await GetByIdAsync(id);
        if (societe != null)
        {
            _dbContext.Societes.Remove(societe);
            await _dbContext.SaveChangesAsync();
        }
    }
}
