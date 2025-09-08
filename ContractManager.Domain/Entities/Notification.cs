using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool EstLue { get; set; }
    public DateTime DateCreation { get; set; }

    public int UtilisateurId { get; set; }
    public Utilisateur? Utilisateur { get; set; }
}