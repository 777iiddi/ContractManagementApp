using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class Avenant
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateModification { get; set; }

    public int ContratId { get; set; }
    public Contrat? Contrat { get; set; }
}