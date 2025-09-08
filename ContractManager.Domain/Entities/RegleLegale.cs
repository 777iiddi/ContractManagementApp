using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class RegleLegale
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DureeMaxMois { get; set; }
    public string ReglesRenouvellement { get; set; } = string.Empty;

    public int PaysId { get; set; }
    public Pays? Pays { get; set; }
}
