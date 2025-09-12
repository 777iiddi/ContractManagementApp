using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Application.Contracts.Persistence;

    public interface IContratValidationService
    {
        Task<IReadOnlyList<string>> ValidateRequiredFieldsAsync(int typeContratId, int? paysId, IDictionary<string, string> champsValeurs);
    }

