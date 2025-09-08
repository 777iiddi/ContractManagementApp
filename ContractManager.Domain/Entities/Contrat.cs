using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ContractManager.Domain.Entities;

public class Contrat
{
    public int Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public DateTime DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string Statut { get; set; } = "En validation";

    // --- Relations (Clés étrangères) ---

    public int EmployeId { get; set; }
    public Employe? Employe { get; set; }

    public int TypeContratId { get; set; }
    public TypeContrat? TypeContrat { get; set; }

    public int SocieteId { get; set; }
    public Societe? Societe { get; set; }

    // --- Relations (Propriétés de navigation) ---

    public ICollection<Avenant> Avenants { get; set; } = new List<Avenant>();
    public Workflow? Workflow { get; set; }
}