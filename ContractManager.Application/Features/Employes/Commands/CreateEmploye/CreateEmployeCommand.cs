using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace ContractManager.Application.Features.Employes.Commands.CreateEmploye;
using MediatR;
public class CreateEmployeCommand : IRequest<int>
{
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
}