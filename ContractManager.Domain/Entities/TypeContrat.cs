namespace ContractManager.Domain.Entities;

// Définit les différents types de contrat possibles (CDI, CDD, etc.).
public class TypeContrat
{
    // Clé primaire.
    public int Id { get; set; }

    // Le nom du type de contrat (ex: "Contrat à Durée Indéterminée").
    public string Nom { get; set; } = string.Empty;
}
