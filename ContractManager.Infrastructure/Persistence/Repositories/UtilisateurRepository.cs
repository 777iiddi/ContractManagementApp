using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public UtilisateurRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Utilisateur?> GetByEmailAsync(string email)
    {
        return await _dbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Utilisateur> AddAsync(Utilisateur utilisateur, CancellationToken cancellationToken)
    {
        await _dbContext.Utilisateurs.AddAsync(utilisateur, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return utilisateur;
    }

    public async Task<IReadOnlyList<Utilisateur>> GetAllAsync()
    {
        return await _dbContext.Utilisateurs.ToListAsync();
    }
}