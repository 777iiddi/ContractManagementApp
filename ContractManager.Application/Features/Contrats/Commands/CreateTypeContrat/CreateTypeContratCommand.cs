using MediatR;

namespace ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;

public class CreateTypeContratCommand : IRequest<int>
{
    public string Nom { get; set; } = string.Empty;
}
