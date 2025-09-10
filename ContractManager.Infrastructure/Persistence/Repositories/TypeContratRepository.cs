using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

    public async Task<TypeContrat?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbContext.TypeContrats
            .Include(tc => tc.ModeleDocument)
            .Include(tc => tc.ChampsObligatoires)
                .ThenInclude(co => co.Pays)
            .FirstOrDefaultAsync(tc => tc.Id == id);
    }

    public async Task<List<TypeContrat>> GetAllAsync()
    {
        return await _dbContext.TypeContrats
            .Where(tc => tc.EstActif)
            .OrderBy(tc => tc.Nom)
            .ToListAsync();
    }

    public async Task<List<TypeContrat>> GetAllWithContractsCountAsync(bool? estActif = null)
    {
        var query = _dbContext.TypeContrats.AsQueryable();

        if (estActif.HasValue)
            query = query.Where(tc => tc.EstActif == estActif.Value);

        return await query
            .Include(tc => tc.Contrats)
            .OrderBy(tc => tc.Nom)
            .ToListAsync();
    }


    public async Task<TypeContrat> AddAsync(TypeContrat typeContrat, CancellationToken cancellationToken)
    {
        await _dbContext.TypeContrats.AddAsync(typeContrat, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return typeContrat;
    }

    public async Task<TypeContrat> UpdateAsync(TypeContrat typeContrat, CancellationToken cancellationToken)
    {
        _dbContext.Entry(typeContrat).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return typeContrat;
    }

    public async Task DeleteAsync(int id)
    {
        var typeContrat = await GetByIdAsync(id);
        if (typeContrat != null)
        {
            typeContrat.EstActif = false; // Soft delete
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<ChampObligatoire>> GetChampsObligatoiresByTypeAndPaysAsync(int typeContratId, int? paysId)
    {
        return await _dbContext.ChampsObligatoires
            .Include(co => co.Pays)
            .Where(co => co.TypeContratId == typeContratId &&
                        (co.PaysId == null || co.PaysId == paysId))
            .OrderBy(co => co.NomChamp)
            .ToListAsync();
    }
}
