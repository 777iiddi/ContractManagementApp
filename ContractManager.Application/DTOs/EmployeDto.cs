using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ContractManager.Application.DTOs;
public class EmployeDto
{
    public int Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string NomComplet { get; set; } = string.Empty;
}
