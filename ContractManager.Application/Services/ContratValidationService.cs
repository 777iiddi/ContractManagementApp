using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.Contracts.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManager.Application.Services;

public class ContratValidationService : IContratValidationService
{
    private readonly ITypeContratRepository _typeContratRepository;

    public ContratValidationService(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<IReadOnlyList<string>> ValidateRequiredFieldsAsync(int typeContratId, int? paysId, IDictionary<string, string> champsValeurs)
    {
        var champs = await _typeContratRepository.GetChampsObligatoiresByTypeAndPaysAsync(typeContratId, paysId);
        var errors = new List<string>();

        foreach (var champ in champs.Where(c => c.EstRequis))
        {
            if (!champsValeurs.TryGetValue(champ.NomChamp, out var value) || string.IsNullOrWhiteSpace(value))
            {
                errors.Add(string.IsNullOrWhiteSpace(champ.MessageErreur)
                    ? $"Champ requis manquant: {champ.NomChamp}"
                    : champ.MessageErreur);
            }
        }
        return errors;
    }
}
