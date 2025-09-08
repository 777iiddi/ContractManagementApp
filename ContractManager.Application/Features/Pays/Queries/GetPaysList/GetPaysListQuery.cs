using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Pays.Queries.GetPaysList;

public class GetPaysListQuery : IRequest<List<PaysDto>> { }