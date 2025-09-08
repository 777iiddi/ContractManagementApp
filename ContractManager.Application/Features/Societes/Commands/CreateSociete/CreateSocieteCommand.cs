using MediatR;

namespace ContractManager.Application.Features.Societes.Commands.CreateSociete;

public class CreateSocieteCommand : IRequest<int>
{
    public string Nom { get; set; } = string.Empty;
}
