using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

// L'implémentation concrète du contrat, qui utilise le DbContext.
public class SocieteRepository : ISocieteRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public SocieteRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Societe>> GetAllAsync()
    {
        return await _dbContext.Societes.ToListAsync();
    }

    // AJOUT : Implémentation de la méthode pour ajouter une société.
    public async Task<Societe> AddAsync(Societe societe, CancellationToken cancellationToken)
    {
        await _dbContext.Societes.AddAsync(societe, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return societe;
    }
    public async Task<Societe?> GetByIdAsync(int id) // AJOUT
    {
        return await _dbContext.Societes.FindAsync(id);
    }
}