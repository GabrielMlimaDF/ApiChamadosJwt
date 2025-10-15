using ChamadosApi.Application.Auth.Services;
using ChamadosApi.Application.Chamados.Services;
using ChamadosApi.Application.Usuarios.Services;
using ChamadosApi.Infrastructure.Persistencia.Data;
using ChamadosApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamadosApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IChamadoService, ChamadoService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}