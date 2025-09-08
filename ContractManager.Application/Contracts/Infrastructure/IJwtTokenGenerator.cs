using ContractManager.Domain.Entities;

namespace ContractManager.Application.Contracts.Infrastructure;

public interface IJwtTokenGenerator
{
    string GenerateToken(Utilisateur utilisateur);
}
