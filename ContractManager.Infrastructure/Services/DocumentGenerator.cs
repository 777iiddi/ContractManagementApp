using ContractManager.Application.Contracts.Infrastructure;
using ContractManager.Domain.Entities;
using Scriban;
using System.Threading.Tasks;

namespace ContractManager.Infrastructure.Services;

public class DocumentGenerator : IDocumentGenerator
{
    public Task<string> GenerateContractPreviewHtml(Contrat contrat, ModeleDocument modele)
    {
        // On prépare un "modèle" Scriban à partir du texte de notre modèle de document
        var template = Template.Parse(modele.ContenuTemplate);

        // On crée un objet qui contient toutes les données que nous voulons rendre disponibles dans le modèle
        var dataContext = new
        {
            // On utilise des noms simples et en minuscules par convention
            contrat = new
            {
                reference = contrat.Reference,
                datedebut = contrat.DateDebut.ToString("dd/MM/yyyy"),
                datefin = contrat.DateFin?.ToString("dd/MM/yyyy") ?? "N/A"
            },
            employe = new
            {
                nom = contrat.Employe.Nom,
                prenom = contrat.Employe.Prenom,
                matricule = contrat.Employe.Matricule
            },
            societe = new
            {
                nom = contrat.Societe.Nom
            }
            // Ajoutez ici d'autres variables si nécessaire (salaire, poste, etc.)
        };

        // On demande à Scriban de "rendre" le modèle en remplaçant les variables par nos données
        var result = template.Render(dataContext);

        return Task.FromResult(result);
    }
}
