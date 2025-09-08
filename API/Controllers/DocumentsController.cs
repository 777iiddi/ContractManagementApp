using ContractManager.Application.Contracts.Infrastructure;
using ContractManager.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentGenerator _documentGenerator;
    private readonly IContratRepository _contratRepository;
    private readonly IModeleDocumentRepository _modeleRepository;

    public DocumentsController(
        IDocumentGenerator documentGenerator,
        IContratRepository contratRepository,
        IModeleDocumentRepository modeleRepository)
    {
        _documentGenerator = documentGenerator;
        _contratRepository = contratRepository;
        _modeleRepository = modeleRepository;
    }

    [HttpGet("contrat/{contratId}/preview")]
    public async Task<IActionResult> GetContratPreview(int contratId)
    {
        var contrat = await _contratRepository.GetByIdAsync(contratId);

        // Pour cet exemple, nous prenons le premier modèle disponible.
        // Plus tard, on liera un contrat à un modèle spécifique.
        var modele = await _modeleRepository.GetFirstAsync();

        if (contrat == null || modele == null)
        {
            return NotFound("Le contrat ou le modèle par défaut est introuvable.");
        }

        var htmlContent = await _documentGenerator.GenerateContractPreviewHtml(contrat, modele);

        // On retourne le résultat en tant que contenu HTML
        return Content(htmlContent, "text/html");
    }
}
