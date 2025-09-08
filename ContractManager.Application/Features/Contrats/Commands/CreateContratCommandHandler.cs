// FICHIER À MODIFIER : ContractManager.Application/Features/Contrats/Commands/CreateContratCommandHandler.cs

using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic; // Ajout pour la liste d'étapes
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Commands.CreateContrat;

public class CreateContratCommandHandler : IRequestHandler<CreateContratCommand, int>
{
    private readonly IContratRepository _contratRepository;
    private readonly IEmployeRepository _employeRepository;
    private readonly ITypeContratRepository _typeContratRepository;
    private readonly ISocieteRepository _societeRepository;
    // --- DÉBUT DES AJOUTS ---
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IEtapeWorkflowRepository _etapeWorkflowRepository;
    // --- FIN DES AJOUTS ---

    public CreateContratCommandHandler(
        IContratRepository contratRepository,
        IEmployeRepository employeRepository,
        ITypeContratRepository typeContratRepository,
        ISocieteRepository societeRepository,
        // --- DÉBUT DES AJOUTS ---
        IWorkflowRepository workflowRepository,
        IEtapeWorkflowRepository etapeWorkflowRepository
        // --- FIN DES AJOUTS ---
        )
    {
        _contratRepository = contratRepository;
        _employeRepository = employeRepository;
        _typeContratRepository = typeContratRepository;
        _societeRepository = societeRepository;
        // --- DÉBUT DES AJOUTS ---
        _workflowRepository = workflowRepository;
        _etapeWorkflowRepository = etapeWorkflowRepository;
        // --- FIN DES AJOUTS ---
    }

    public async Task<int> Handle(CreateContratCommand request, CancellationToken cancellationToken)
    {
        // --- VÉRIFICATIONS (inchangées) ---
        var employe = await _employeRepository.GetByIdAsync(request.EmployeId);
        if (employe == null)
        {
            throw new Exception($"L'employé avec l'ID {request.EmployeId} n'existe pas.");
        }

        var typeContrat = await _typeContratRepository.GetByIdAsync(request.TypeContratId);
        if (typeContrat == null)
        {
            throw new Exception($"Le type de contrat avec l'ID {request.TypeContratId} n'existe pas.");
        }

        var societe = await _societeRepository.GetByIdAsync(request.SocieteId);
        if (societe == null)
        {
            throw new Exception($"La société avec l'ID {request.SocieteId} n'existe pas.");
        }
        // --- FIN DES VÉRIFICATIONS ---

        var contrat = new Contrat
        {
            Reference = request.Reference,
            DateDebut = request.DateDebut,
            DateFin = request.DateFin,
            Statut = "En validation", // Le statut du contrat reste "En validation"
            EmployeId = request.EmployeId,
            TypeContratId = request.TypeContratId,
            SocieteId = request.SocieteId
        };

        var newContrat = await _contratRepository.AddAsync(contrat, cancellationToken);

        // --- DÉBUT DE LA LOGIQUE DE WORKFLOW AJOUTÉE ---

        // 1. Créer le conteneur du workflow
        var workflow = new Workflow
        {
            ContratId = newContrat.Id,
            Statut = "En cours", // Le statut du workflow est "En cours"
            Etapes = new List<EtapeWorkflow>() // Initialiser la collection d'étapes
        };

        // 2. Créer la première étape pour le Manager
        var etapeManager = new EtapeWorkflow
        {
            Ordre = 1,
            Statut = "En attente", // L'étape est en attente de l'action du manager
            RoleValidateur = "Manager",
            Commentaire = string.Empty
        };

        // 3. (Optionnel) Créer une deuxième étape pour les RH, qui sera activée plus tard
        var etapeRH = new EtapeWorkflow
        {
            Ordre = 2,
            Statut = "Non démarré", // Cette étape n'est pas encore active
            RoleValidateur = "RH",
            Commentaire = string.Empty
        };

        // On ajoute les étapes au workflow
        workflow.Etapes.Add(etapeManager);
        workflow.Etapes.Add(etapeRH);

        // On sauvegarde le workflow et ses étapes dans la base de données
        await _workflowRepository.AddAsync(workflow, cancellationToken);

        // --- FIN DE LA LOGIQUE DE WORKFLOW AJOUTÉE ---

        return newContrat.Id;
    }
}