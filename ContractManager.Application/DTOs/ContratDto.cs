namespace ContractManager.Application.DTOs;

// This is a Data Transfer Object (DTO).
// It's a simple class used to transfer data from the application to the API layer,
// and ultimately to the client. We use it to shape the data and avoid exposing our domain entities.
public class ContratDto
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }

    // We can flatten the data for simplicity.
    public string NomEmploye { get; set; } = string.Empty;
    public string TypeDeContrat { get; set; } = string.Empty;
}