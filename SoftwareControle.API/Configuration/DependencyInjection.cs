using SoftwareControle.Repositório;
using SoftwareControle.Repositorio.Context;
using SoftwareControle.Services.Services.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftwareControle.Repository.Repositorio.Ordem;
using SoftwareControle.Repository.Repositorio.Usuario;
using System.Text;
using HFAcademia.Repositório;
using Application.Services.Auth;
using SoftwareControle.Service.Services.Ferramenta;
using FluentValidation;
using SoftwareControle.Services.Validacao;
using SoftwareControle.Models;
using SoftwareControle.Service.Validacao;
using SoftwareControle.Repositorio.Repositorio.Relatorio;
using SoftwareControle.Service.Services.Relatorio;
using WebUi.Middlewares;

namespace SoftwareControle.WebUi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
    {
        //Repositorio
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        services.AddScoped<IFerramentaRepositorio, FerramentaRepositorio>();
        services.AddScoped<IOrdemRepositorio, OrdemRepositorio>();
        services.AddScoped<IRelatorioRepositorio, RelatorioRepositorio>();

        //Services
        services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IFerramentaService, FerramentaService>();
		services.AddScoped<IOrdemService, OrdemService>();
		services.AddScoped<IRelatorioService, RelatorioService>();

        //Validation
        services.AddSingleton<IValidator<UsuarioModel>, UsuarioValidacao>();
        services.AddSingleton<IValidator<FerramentaModel>, FerramentaValidacao>();
        services.AddSingleton<IValidator<OrdemModel>, OrdemValidacao>();
        services.AddSingleton<IValidator<RelatorioModel>, RelatorioValidacao>();

		services.AddTransient<GlobalExceptionHandlingMiddleware>();

		return services;
    }

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("SoftwareControleConnection"),
                assembly => assembly.MigrationsAssembly(typeof(ApplicationDbContext)
                .Assembly.FullName));
        });

        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(config["Jwt:key"]!))
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(policy =>
        {
            policy.AddPolicy("OpenCorsPolicy", opt =>
                opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });

        return services;
    }
}
