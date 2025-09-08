using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Application.DTOs;
public class WorkflowDto
{
    public int Id { get; set; }
    public string Statut { get; set; } = string.Empty;
    public List<EtapeWorkflowDto> Etapes { get; set; } = new();
}
