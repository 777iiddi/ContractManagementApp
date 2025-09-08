using System.Diagnostics.Contracts;

namespace ContractManager.Domain.Entities;

// Définit l'entité Employe. C'est le plan pour chaque employé dans le système.
public class Employe
{
    // Clé primaire, identifiant unique pour chaque employé.
    public int Id { get; set; }

    // Matricule unique de l'employé.
    public string Matricule { get; set; } = string.Empty;

    // Nom de famille de l'employé.
    public string Nom { get; set; } = string.Empty;

    // Prénom de l'employé.
    public string Prenom { get; set; } = string.Empty;

    // Propriété de navigation : Un employé peut avoir une collection de contrats.
    // C'est la relation "un-à-plusieurs".
    public int? ManagerId { get; set; }
    public Utilisateur? Manager { get; set; }
    public ICollection<Contrat> Contrats { get; set; } = new List<Contrat>();
}