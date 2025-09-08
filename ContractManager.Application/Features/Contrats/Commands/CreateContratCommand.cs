using MediatR;
using System;

namespace ContractManager.Application.Features.Contrats.Commands.CreateContrat;

public class CreateContratCommand : IRequest<int>
{
    public string Reference { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public int EmployeId { get; set; }
    public int TypeContratId { get; set; }
    public int SocieteId { get; set; }
}