using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; // Assurez-vous d'avoir ce using
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class EtapeWorkflowRepository : IEtapeWorkflowRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public EtapeWorkflowRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EtapeWorkflow?> GetByIdAsync(int id)
    {
        return await _dbContext.EtapeWorkflows.Include(e => e.Workflow).ThenInclude(w => w.Etapes).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(EtapeWorkflow etapeWorkflow)
    {
        _dbContext.Entry(etapeWorkflow).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<EtapeWorkflow> AddAsync(EtapeWorkflow etapeWorkflow, CancellationToken cancellationToken)
    {
        await _dbContext.EtapeWorkflows.AddAsync(etapeWorkflow, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return etapeWorkflow;
    }
}
