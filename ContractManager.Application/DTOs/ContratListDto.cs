using System;

namespace ContractManager.Application.DTOs;

public class ContratListDto
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }
    public string? NomEmploye { get; set; }
    public string? TypeDeContrat { get; set; }
}
