using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratList;

public class GetTypeContratListQuery : IRequest<List<TypeContratDto>> { }
