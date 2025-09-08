using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Contrats.Queries.GetContratList;

public class GetContratListQuery : IRequest<List<ContratListDto>> { }
