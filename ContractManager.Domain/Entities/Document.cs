using System;

namespace ContractManager.Domain.Entities;

public class Document
{
    public int Id { get; set; }

    public int ContratId { get; set; }
    public Contrat Contrat { get; set; } = null!;

    public int ModeleDocumentId { get; set; }
    public ModeleDocument ModeleDocument { get; set; } = null!;

    public string Nom { get; set; } = string.Empty;

    public string ContenuHtml { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; }
}
