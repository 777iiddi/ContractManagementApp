using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractManager.Application.DTOs; 
using MediatR;
namespace ContractManager.Application.Features.Employes.Queries.GetEmployeList;
public class GetEmployeListQuery : IRequest<List<EmployeDto>> { }

