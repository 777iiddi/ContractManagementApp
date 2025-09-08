using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class ModeleDocumentRepository : IModeleDocumentRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public ModeleDocumentRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ModeleDocument?> GetByIdAsync(int id)
    {
        return await _dbContext.ModeleDocuments.FindAsync(id);
    }

    public async Task<IReadOnlyList<ModeleDocument>> GetAllAsync()
    {
        return await _dbContext.ModeleDocuments.ToListAsync();
    }

    public async Task<ModeleDocument> AddAsync(ModeleDocument modele, CancellationToken cancellationToken)
    {
        await _dbContext.ModeleDocuments.AddAsync(modele, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return modele;
    }

    public async Task UpdateAsync(ModeleDocument modele)
    {
        _dbContext.Entry(modele).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    public async Task<ModeleDocument?> GetFirstAsync()
    {
        return await _dbContext.ModeleDocuments.FirstOrDefaultAsync();
    }

}
