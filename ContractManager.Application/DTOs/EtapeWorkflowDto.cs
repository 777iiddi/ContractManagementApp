using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Application.DTOs;
public class EtapeWorkflowDto
{
    public int Id { get; set; }
    public int Ordre { get; set; }
    public string Statut { get; set; } = string.Empty;
    public string RoleValidateur { get; set; } = string.Empty;
}
