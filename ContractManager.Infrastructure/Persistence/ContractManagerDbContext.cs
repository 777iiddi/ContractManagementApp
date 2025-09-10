using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Persistence;

// This class inherits from DbContext, which is the main class from Entity Framework.
public class ContractManagerDbContext : DbContext
{
    // The constructor receives DbContextOptions, which will be used to configure
    // the connection to the database (e.g., in-memory, SQL Server, etc.).
    public ContractManagerDbContext(DbContextOptions<ContractManagerDbContext> options) : base(options)
    {
    }
    public ContractManagerDbContext()
    {
    }
    // --- DbSets ---
    // Each DbSet<T> property tells Entity Framework that we want a table for the entity 'T'.
    // The name of the property will be the name of the table in the database.

    // Entités de base
    public DbSet<Contrat> Contrats { get; set; }
    public DbSet<Employe> Employes { get; set; }
    public DbSet<TypeContrat> TypeContrats { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Societe> Societes { get; set; }
    public DbSet<Pays> Pays { get; set; }
    public DbSet<RegleLegale> ReglesLegales { get; set; }

    // Entités de modèles et avenants
    public DbSet<ModeleDocument> ModeleDocuments { get; set; }
    public DbSet<Variable> Variables { get; set; }
    public DbSet<Avenant> Avenants { get; set; }

    // Entités de workflow et traçabilité
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<EtapeWorkflow> EtapeWorkflows { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ChampObligatoire> ChampsObligatoires { get; set; }
    public DbSet<RegleTypeContrat> ReglesTypeContrats { get; set; }
    // This method is called by Entity Framework when it creates the database model.
    // It's a good place to add specific configurations or seed initial data.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Example of seeding data: We add the default contract types when the database is created.
        modelBuilder.Entity<TypeContrat>().HasData(
            new TypeContrat { Id = 1, Nom = "CDI" },
            new TypeContrat { Id = 2, Nom = "CDD" },
            new TypeContrat { Id = 3, Nom = "Stage" },
            new TypeContrat { Id = 4, Nom = "Intérim" }
        );
        modelBuilder.Entity<ChampObligatoire>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NomChamp).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TypeChamp).IsRequired().HasMaxLength(50);

            entity.HasOne(e => e.TypeContrat)
                .WithMany(tc => tc.ChampsObligatoires)
                .HasForeignKey(e => e.TypeContratId);

            entity.HasOne(e => e.Pays)
                .WithMany(p => p.ChampsObligatoires)
                .HasForeignKey(e => e.PaysId);
        });

        // Configuration RegleTypeContrat
        modelBuilder.Entity<RegleTypeContrat>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.TypeContrat)
                .WithMany(tc => tc.ReglesLegales)
                .HasForeignKey(e => e.TypeContratId);

            entity.HasOne(e => e.Pays)
                .WithMany(p => p.ReglesLegales)
                .HasForeignKey(e => e.PaysId);
        });

        // Configuration TypeContrat
        modelBuilder.Entity<TypeContrat>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.ModeleDocument)
                .WithMany()
                .HasForeignKey(e => e.ModeleDocumentId);
        });
        modelBuilder.Entity<Societe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nom).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Adresse).HasMaxLength(500);
            entity.Property(e => e.CodePostal).HasMaxLength(10);
            entity.Property(e => e.Ville).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Telephone).HasMaxLength(20);

            // Relation avec Pays
            entity.HasOne(e => e.Pays)
                .WithMany(p => p.Societes)
                .HasForeignKey(e => e.PaysId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // This ensures that any configuration from the base class is also applied.
        base.OnModelCreating(modelBuilder);
    }
}
