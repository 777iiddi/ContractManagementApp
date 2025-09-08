using System;
using System.Collections.Generic;

namespace ContractManager.Application.DTOs;

public class ContratDetailDto
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string NomEmploye { get; set; } = string.Empty;
    public string TypeDeContrat { get; set; } = string.Empty;
    public string NomSociete { get; set; } = string.Empty;
    public WorkflowDto? Workflow { get; set; }
}