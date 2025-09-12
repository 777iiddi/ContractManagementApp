using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ContractManager.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContractManagerDbContext>
    {
        public ContractManagerDbContext CreateDbContext(string[] args)
        {
            // Construire la configuration à partir du fichier appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Récupérer la chaîne de connexion
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("La chaîne de connexion 'DefaultConnection' est introuvable.");

            // Configurer le DbContextOptionsBuilder avec le fournisseur MySQL
            var optionsBuilder = new DbContextOptionsBuilder<ContractManagerDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // Retourner une instance de DbContext configurée
            return new ContractManagerDbContext(optionsBuilder.Options);
        }
    }
}
