using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface ITypeContratRepository
{
    Task<TypeContrat?> GetByIdAsync(int id);
    Task<TypeContrat?> GetByIdWithDetailsAsync(int id);
    Task<List<TypeContrat>> GetAllAsync();
    Task<List<TypeContrat>> GetAllWithContractsCountAsync(bool? estActif = null);
    Task<TypeContrat> AddAsync(TypeContrat typeContrat, CancellationToken cancellationToken);
    Task<TypeContrat> UpdateAsync(TypeContrat typeContrat, CancellationToken cancellationToken);
    Task DeleteAsync(int id);
    Task<List<ChampObligatoire>> GetChampsObligatoiresByTypeAndPaysAsync(int typeContratId, int? paysId);
}
