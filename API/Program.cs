using ContractManager.Application;
using ContractManager.Application.Contracts.Infrastructure;
using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Settings;
using ContractManager.Infrastructure.Persistence;
using ContractManager.Infrastructure.Persistence.Repositories;
using ContractManager.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICES CONFIGURATION ---

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(Options.Create(jwtSettings));

builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContractManagerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Repositories ---
builder.Services.AddScoped<IContratRepository, ContratRepository>();
builder.Services.AddScoped<IEmployeRepository, EmployeRepository>();
builder.Services.AddScoped<ISocieteRepository, SocieteRepository>();
builder.Services.AddScoped<IPaysRepository, PaysRepository>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IEtapeWorkflowRepository, EtapeWorkflowRepository>();
builder.Services.AddScoped<IAvenantRepository, AvenantRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<ITypeContratRepository, TypeContratRepository>();
builder.Services.AddScoped<ITypeContratRepository, TypeContratRepository>();
builder.Services.AddScoped<IModeleDocumentRepository, ModeleDocumentRepository>();
builder.Services.AddScoped<IDocumentGenerator, DocumentGenerator>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(Assembly.Load("ContractManager.Application"));

builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    });

// --- APP BUILD & MIDDLEWARE ---

var app = builder.Build();


// Apply migrations & seed DB
ApplyMigrationsAndSeed(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// --- MIGRATIONS + SEEDING ---
void ApplyMigrationsAndSeed(IHost app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ContractManagerDbContext>();

    // ✅ This will create DB if it doesn’t exist & apply migrations without deleting data
    context.Database.Migrate();

    // --- SEEDING INITIAL DATA (only if tables are empty) ---
    if (!context.TypeContrats.Any())
    {
        context.TypeContrats.AddRange(
            new TypeContrat { Nom = "CDI" },
            new TypeContrat { Nom = "CDD" },
            new TypeContrat { Nom = "Stage" }
        );
    }

    if (!context.Societes.Any())
    {
        context.Societes.Add(new Societe { Nom = "Ma Société" });
    }

    if (!context.Employes.Any())
    {
        context.Employes.AddRange(
            new Employe { Nom = "Dupont", Prenom = "Jean", Matricule = "E001" },
            new Employe { Nom = "Martin", Prenom = "Marie", Matricule = "E002" }
        );
    }

    if (!context.Utilisateurs.Any())
    {
        context.Utilisateurs.Add(new Utilisateur
        {
            Email = "admin@test.com",
            MotDePasseHash = BCrypt.Net.BCrypt.HashPassword("password"),
            Role = "Admin"
        });
    }

    context.SaveChanges();
}
