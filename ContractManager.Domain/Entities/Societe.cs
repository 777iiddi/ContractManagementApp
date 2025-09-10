using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractManager.Domain.Entities;

public class Societe
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = string.Empty;

    public string? Adresse { get; set; } = string.Empty;

    public string? CodePostal { get; set; } = string.Empty;

    public string? Ville { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string? Telephone { get; set; } = string.Empty;

    public int? PaysId { get; set; }
    public Pays? Pays { get; set; }

    public ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();
}
