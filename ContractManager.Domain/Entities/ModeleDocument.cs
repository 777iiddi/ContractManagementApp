using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class ModeleDocument
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string ContenuTemplate { get; set; } = string.Empty;
}