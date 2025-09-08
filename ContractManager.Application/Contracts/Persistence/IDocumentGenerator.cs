using ContractManager.Domain.Entities;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Infrastructure;

public interface IDocumentGenerator
{
    Task<string> GenerateContractPreviewHtml(Contrat contrat, ModeleDocument modele);
}
