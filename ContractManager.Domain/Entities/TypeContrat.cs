using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractManager.Domain.Entities;

public class TypeContrat
{
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; } = string.Empty; // CDI, CDD, Stage, Intérim, Freelance

    public string Description { get; set; } = string.Empty;

    public bool EstActif { get; set; } = true;

    // Durée par défaut en mois (null = indéterminée)
    public int? DureeDefautMois { get; set; }

    // Période d'essai par défaut en jours
    public int? PeriodeEssaiDefautJours { get; set; }

    // Préavis par défaut en jours
    public int? PreavisDefautJours { get; set; }

    // Modèle de document associé
    public int? ModeleDocumentId { get; set; }
    public ModeleDocument? ModeleDocument { get; set; }

    // Relations
    public ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();
    public ICollection<ChampObligatoire> ChampsObligatoires { get; set; } = new List<ChampObligatoire>();
    public ICollection<RegleTypeContrat> ReglesLegales { get; set; } = new List<RegleTypeContrat>();
}

// Nouvelle entité pour les champs obligatoires
public class ChampObligatoire
{
    public int Id { get; set; }

    [Required]
    public string NomChamp { get; set; } = string.Empty; // DateFin, SalaireBase, etc.

    public string TypeChamp { get; set; } = string.Empty; // Date, Decimal, String, Boolean

    public bool EstRequis { get; set; } = true;

    public string? ValeurParDefaut { get; set; }

    public string? ValidationRegex { get; set; }

    public string? MessageErreur { get; set; }

    // Relations
    public int TypeContratId { get; set; }
    public TypeContrat TypeContrat { get; set; } = null!;

    public int? PaysId { get; set; }
    public Pays? Pays { get; set; }
}

// Nouvelle entité pour les règles légales par type et pays
public class RegleTypeContrat
{
    public int Id { get; set; }

    public int TypeContratId { get; set; }
    public TypeContrat TypeContrat { get; set; } = null!;

    public int PaysId { get; set; }
    public Pays Pays { get; set; } = null!;

    public int? DureeMaxMois { get; set; } // Durée max autorisée
    public int? PeriodeEssaiMaxJours { get; set; }
    public int? PreavisMinJours { get; set; }

    public decimal? SalaireMinimum { get; set; }
    public string? AutresRegles { get; set; } // JSON pour règles spécifiques
}
