using ContractManager.Domain.Entities;
using System.Collections.Generic; // AJOUT
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IUtilisateurRepository
{
    Task<Utilisateur?> GetByEmailAsync(string email);
    Task<Utilisateur> AddAsync(Utilisateur utilisateur, CancellationToken cancellationToken);
    Task<IReadOnlyList<Utilisateur>> GetAllAsync(); 
}