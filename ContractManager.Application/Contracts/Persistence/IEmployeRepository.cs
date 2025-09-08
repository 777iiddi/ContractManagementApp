using ContractManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

// Le contrat qui définit les opérations possibles pour les employés.
public interface IEmployeRepository
{
    // AJOUT : Méthode pour récupérer tous les employés.
    Task<IReadOnlyList<Employe>> GetAllAsync();

    Task<Employe> AddAsync(Employe employe);
    Task<Employe?> GetByIdAsync(int id); // AJOUT

}