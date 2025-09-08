using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Auth.Queries.GetUserList;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserDto>>
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public GetUserListQueryHandler(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task<List<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _utilisateurRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Email = u.Email,
            Role = u.Role
        }).ToList();
    }
}
