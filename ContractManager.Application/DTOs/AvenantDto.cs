using System;

namespace ContractManager.Application.DTOs;

public class AvenantDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateModification { get; set; }
}
