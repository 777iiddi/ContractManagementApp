using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class EtapeWorkflow
{
    public int Id { get; set; }
    public int Ordre { get; set; }
    public string Statut { get; set; } = string.Empty; 
    public DateTime? DateAction { get; set; }
    public string Commentaire { get; set; } = string.Empty;
    public string RoleValidateur { get; set; } = string.Empty;
    public int WorkflowId { get; set; }
    public Workflow? Workflow { get; set; }

    public int? UtilisateurId { get; set; } 
    public Utilisateur? Utilisateur { get; set; }
}
