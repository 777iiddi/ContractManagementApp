using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories
{
    public class ContratRepository : IContratRepository
    {
        private readonly ContractManagerDbContext _dbContext;

        public ContratRepository(ContractManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contrat?> GetByIdAsync(int id)
        {
            return await _dbContext.Contrats
                .Include(c => c.Employe)
                .Include(c => c.TypeContrat)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Contrat>> GetAllContractsAsync()
        {
            return await _dbContext.Contrats
                .Include(c => c.Employe)
                .Include(c => c.TypeContrat)
                .ToListAsync();
        }

        public async Task<List<Contrat>> GetContractsForValidationAsync()
        {
            return await _dbContext.Contrats
                .Where(c => c.Statut == "En validation")
                .Include(c => c.Employe)
                .Include(c => c.TypeContrat)
                .ToListAsync();
        }

        public async Task<List<Contrat>> GetContractsForValidationByManagerIdAsync(int managerId)
        {
            // First try to get contracts for employees under this manager
            var contractsWithManager = await _dbContext.Contrats
                .Include(c => c.Employe)
                .Include(c => c.TypeContrat)
                .Where(c => c.Employe.ManagerId == managerId && c.Statut == "En validation")
                .ToListAsync();

            // If no contracts found, return all contracts pending validation
            if (!contractsWithManager.Any())
            {
                return await _dbContext.Contrats
                    .Include(c => c.Employe)
                    .Include(c => c.TypeContrat)
                    .Where(c => c.Statut == "En validation")
                    .ToListAsync();
            }

            return contractsWithManager;
        }

        public async Task<Contrat> AddAsync(Contrat contrat, CancellationToken cancellationToken)
        {
            await _dbContext.Contrats.AddAsync(contrat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return contrat;
        }

        public async Task<Contrat> UpdateAsync(Contrat contrat, CancellationToken cancellationToken)
        {
            _dbContext.Entry(contrat).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return contrat;
        }

        public async Task UpdateAsync(Contrat contrat)
        {
            _dbContext.Entry(contrat).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
