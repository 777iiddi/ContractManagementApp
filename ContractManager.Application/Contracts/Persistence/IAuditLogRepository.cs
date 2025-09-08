using ContractManager.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken);
}
