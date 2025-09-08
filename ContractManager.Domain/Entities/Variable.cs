﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractManager.Domain.Entities;
public class Variable
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty; // Ex: "{{NomEmploye}}"
    public string Description { get; set; } = string.Empty;
}