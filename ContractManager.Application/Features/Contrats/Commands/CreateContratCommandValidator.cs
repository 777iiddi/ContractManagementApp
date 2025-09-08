using FluentValidation;

namespace ContractManager.Application.Features.Contrats.Commands.CreateContrat;

// Cette classe hérite de AbstractValidator et spécifie qu'elle valide des objets CreateContratCommand.
public class CreateContratCommandValidator : AbstractValidator<CreateContratCommand>
{
    public CreateContratCommandValidator()
    {
        // Règle 1 : La référence ne doit pas être vide.
        RuleFor(p => p.Reference)
            .NotEmpty().WithMessage("{PropertyName} est requis.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} ne doit pas dépasser 50 caractères.");

        // Règle 2 : La date de début est obligatoire.
        RuleFor(p => p.DateDebut)
            .NotEmpty().WithMessage("{PropertyName} est requise.")
            .NotNull();

        // Règle 3 : L'ID de l'employé doit être un nombre positif.
        RuleFor(p => p.EmployeId)
            .GreaterThan(0).WithMessage("{PropertyName} doit être supérieur à 0.");

        // Règle 4 : L'ID du type de contrat doit être un nombre positif.
        RuleFor(p => p.TypeContratId)
            .GreaterThan(0).WithMessage("{PropertyName} doit être supérieur à 0.");

        // Règle 5 : L'ID de la société doit être un nombre positif.
        RuleFor(p => p.SocieteId)
            .GreaterThan(0).WithMessage("{PropertyName} doit être supérieur à 0.");
    }
}
