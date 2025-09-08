using MediatR;

namespace ContractManager.Application.Features.Pays.Commands.CreatePays;

public class CreatePaysCommand : IRequest<int>
{
    public string Nom { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
