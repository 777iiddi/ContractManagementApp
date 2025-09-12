using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Persistence.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ContractManagerDbContext _dbContext;

    public DocumentRepository(ContractManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Document> AddAsync(Document document, CancellationToken cancellationToken)
    {
        await _dbContext.Documents.AddAsync(document, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return document;
    }

    public async Task<Document?> GetByIdAsync(int id)
    {
        return await _dbContext.Documents
            .Include(d => d.Contrat)
            .Include(d => d.ModeleDocument)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IReadOnlyList<Document>> GetByContratIdAsync(int contratId)
    {
        return await _dbContext.Documents
            .Where(d => d.ContratId == contratId)
            .ToListAsync();
    }
}
