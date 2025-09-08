using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface ITypeContratRepository
{
    Task<TypeContrat?> GetByIdAsync(int id);
    Task<IReadOnlyList<TypeContrat>> GetAllAsync();
    Task<TypeContrat> AddAsync(TypeContrat typeContrat, CancellationToken cancellationToken); // AJOUT : Nouvelle méthode

}
