using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

// L'implémentation qui parle à la base de données.
public class EmployeRepository : IEmployeRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public EmployeRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // AJOUT : Implémentation de la méthode pour récupérer tous les employés.
    public async Task<IReadOnlyList<Employe>> GetAllAsync()
    {
        return await _dbContext.Employes.ToListAsync();
    }

    public async Task<Employe> AddAsync(Employe employe)
    {
        await _dbContext.Employes.AddAsync(employe);
        await _dbContext.SaveChangesAsync();
        return employe;
    }
    public async Task<Employe?> GetByIdAsync(int id) 
    {
        return await _dbContext.Employes.FindAsync(id);
    }
}
