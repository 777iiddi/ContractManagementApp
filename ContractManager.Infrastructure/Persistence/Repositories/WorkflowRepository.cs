using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public WorkflowRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Workflow> AddAsync(Workflow workflow, CancellationToken cancellationToken)
    {
        await _dbContext.Workflows.AddAsync(workflow, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return workflow;
    }
}
