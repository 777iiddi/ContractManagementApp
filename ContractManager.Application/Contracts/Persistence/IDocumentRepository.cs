using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IDocumentRepository
{
    Task<Document> AddAsync(Document document, CancellationToken cancellationToken);
    Task<Document?> GetByIdAsync(int id);
    Task<IReadOnlyList<Document>> GetByContratIdAsync(int contratId);
}
