using ContractManager.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

public interface IEtapeWorkflowRepository
{
    Task<EtapeWorkflow?> GetByIdAsync(int id); // AJOUT : Pour trouver une étape spécifique
    Task UpdateAsync(EtapeWorkflow etapeWorkflow); // AJOUT : Pour sauvegarder les changements
    Task<EtapeWorkflow> AddAsync(EtapeWorkflow etapeWorkflow, CancellationToken cancellationToken);
}