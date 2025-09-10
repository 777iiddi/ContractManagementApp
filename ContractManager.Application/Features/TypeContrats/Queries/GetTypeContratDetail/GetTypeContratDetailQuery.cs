using ContractManager.Application.DTOs;
using MediatR;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratDetail;

public class GetTypeContratDetailQuery : IRequest<TypeContratDto>
{
    public int Id { get; set; }
}
