namespace ContractManager.Domain.Entities;

public class Utilisateur
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string MotDePasseHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Ex: "RH", "Manager"
}
