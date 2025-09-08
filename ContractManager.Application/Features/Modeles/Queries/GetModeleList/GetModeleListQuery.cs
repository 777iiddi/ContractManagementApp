using ContractManager.Application.Features.Modeles.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Modeles.Queries.GetModeleList;

public class GetModeleListQuery : IRequest<List<ModeleDocumentDto>> { }
