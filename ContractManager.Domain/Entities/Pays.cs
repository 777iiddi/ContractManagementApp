using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractManager.Domain.Entities;

public class Pays
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty; // FR, US, DE, etc.

    public string? CodeISO { get; set; } = string.Empty; // FRA, USA, DEU

    public string Devise { get; set; } = "EUR";

    public bool EstActif { get; set; } = true;

    // Relations
    public ICollection<Societe> Societes { get; set; } = new List<Societe>();
    public ICollection<ChampObligatoire> ChampsObligatoires { get; set; } = new List<ChampObligatoire>();
    public ICollection<RegleTypeContrat> ReglesLegales { get; set; } = new List<RegleTypeContrat>();
}
