using MediatR;

namespace ContractManager.Application.Features.Modeles.Commands.CreateOrUpdateModele;

// On utilise une seule commande pour la création et la mise à jour.
// Si l'Id est 0, c'est une création. Sinon, c'est une mise à jour.
public class CreateOrUpdateModeleCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string ContenuTemplate { get; set; } = string.Empty;
}
