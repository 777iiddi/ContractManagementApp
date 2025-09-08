using ContractManager.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IWorkflowRepository
{
    Task<Workflow> AddAsync(Workflow workflow, CancellationToken cancellationToken);
}
