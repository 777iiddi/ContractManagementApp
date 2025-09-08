using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class Workflow
{
    public int Id { get; set; }
    public string Statut { get; set; } = string.Empty; // Ex: "En cours", "Validé"

    public int ContratId { get; set; }
    public Contrat? Contrat { get; set; }
    public ICollection<EtapeWorkflow> Etapes { get; set; } = new List<EtapeWorkflow>();
}