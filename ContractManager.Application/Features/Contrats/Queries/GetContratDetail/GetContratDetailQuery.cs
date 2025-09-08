using ContractManager.Application.DTOs;
using MediatR;

namespace ContractManager.Application.Features.Contrats.Queries.GetContratDetail;

public class GetContratDetailQuery : IRequest<ContratDetailDto>
{
    public int Id { get; set; }
}
