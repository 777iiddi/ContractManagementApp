using MediatR;
using System;

namespace ContractManager.Application.Features.Avenants.Commands.CreateAvenant;

public class CreateAvenantCommand : IRequest<int>
{
    public int ContratId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateModification { get; set; }
}
