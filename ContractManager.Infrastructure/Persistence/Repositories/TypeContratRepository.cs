using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class TypeContratRepository : ITypeContratRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public TypeContratRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TypeContrat?> GetByIdAsync(int id)
    {
        return await _dbContext.TypeContrats.FindAsync(id);
    }

    public async Task<IReadOnlyList<TypeContrat>> GetAllAsync()
    {
        return await _dbContext.TypeContrats.ToListAsync();
    }
    public async Task<TypeContrat> AddAsync(TypeContrat typeContrat, CancellationToken cancellationToken)
    {
        await _dbContext.TypeContrats.AddAsync(typeContrat, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return typeContrat;
    }
}
