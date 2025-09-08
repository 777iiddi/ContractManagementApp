using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Contrats.Queries.GetContractsForValidation;

public class GetContractsForValidationQuery : IRequest<List<ContratListDto>>
{
}