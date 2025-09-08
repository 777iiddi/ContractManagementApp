namespace ContractManager.Application.DTOs;

// Ce DTO est utilisé pour transférer les données des types de contrat.
public class TypeContratDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
}
