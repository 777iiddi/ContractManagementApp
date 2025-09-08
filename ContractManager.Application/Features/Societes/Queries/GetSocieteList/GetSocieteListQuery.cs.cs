using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Societes.Queries.GetSocieteList;

public class GetSocieteListQuery : IRequest<List<SocieteDto>> { }
