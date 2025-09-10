using System.Collections.Generic;

namespace ContractManager.Application.DTOs;

public class TypeContratDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool EstActif { get; set; }
    public int? DureeDefautMois { get; set; }
    public int? PeriodeEssaiDefautJours { get; set; }
    public int? PreavisDefautJours { get; set; }
    public int? ModeleDocumentId { get; set; }
    public string? ModeleDocumentNom { get; set; }
    public List<ChampObligatoireDto> ChampsObligatoires { get; set; } = new();
}

public class TypeContratListDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool EstActif { get; set; }
    public int NombreContrats { get; set; }
}

public class ChampObligatoireDto
{
    public int Id { get; set; }
    public string NomChamp { get; set; } = string.Empty;
    public string TypeChamp { get; set; } = string.Empty;
    public bool EstRequis { get; set; }
    public string? ValeurParDefaut { get; set; }
    public string? ValidationRegex { get; set; }
    public string? MessageErreur { get; set; }
    public int? PaysId { get; set; }
    public string? PaysNom { get; set; }
}

public class CreateTypeContratDto
{
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? DureeDefautMois { get; set; }
    public int? PeriodeEssaiDefautJours { get; set; }
    public int? PreavisDefautJours { get; set; }
    public int? ModeleDocumentId { get; set; }
    public List<CreateChampObligatoireDto> ChampsObligatoires { get; set; } = new();
}

public class CreateChampObligatoireDto
{
    public string NomChamp { get; set; } = string.Empty;
    public string TypeChamp { get; set; } = string.Empty;
    public bool EstRequis { get; set; } = true;
    public string? ValeurParDefaut { get; set; }
    public string? ValidationRegex { get; set; }
    public string? MessageErreur { get; set; }
    public int? PaysId { get; set; }
}
