using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IModeleDocumentRepository
{
    Task<ModeleDocument?> GetByIdAsync(int id);
    Task<IReadOnlyList<ModeleDocument>> GetAllAsync();
    Task<ModeleDocument> AddAsync(ModeleDocument modele, CancellationToken cancellationToken);
    Task UpdateAsync(ModeleDocument modele);
    Task<ModeleDocument?> GetFirstAsync();
} 
