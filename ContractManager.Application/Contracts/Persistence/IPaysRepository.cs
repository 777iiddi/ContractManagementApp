// IPaysRepository.cs
using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IPaysRepository
{
    Task<Pays?> GetByIdAsync(int id);
    Task<List<Pays>> GetAllAsync();
    Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken);
    Task<Pays> UpdateAsync(Pays pays, CancellationToken cancellationToken);
}
