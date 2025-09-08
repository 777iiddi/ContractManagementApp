using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class AuditLog
{
    public int Id { get; set; }
    public string Action { get; set; } = string.Empty; // Ex: "Création Contrat"
    public DateTime DateAction { get; set; }
    public int EntiteId { get; set; } // ID de l'entité concernée (ex: ContratId)

    public int UtilisateurId { get; set; }
    public Utilisateur? Utilisateur { get; set; }
    public string Details { get; set; }
}