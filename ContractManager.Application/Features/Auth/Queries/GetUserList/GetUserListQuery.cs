using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Auth.Queries.GetUserList;

public class GetUserListQuery : IRequest<List<UserDto>> { }
