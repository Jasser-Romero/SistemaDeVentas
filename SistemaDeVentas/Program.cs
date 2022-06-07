using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaDeVentas.AppCore.Interfaces;
using SistemaDeVentas.AppCore.Services;
using SistemaDeVentas.Domain.Interfaces;
using SistemaDeVentas.Domain.SistemaDeVentasEntities;
using SistemaDeVentas.Formularios;
using SistemaDeVentas.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeVentas
{
    internal static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();
                
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddDbContext<SistemaDeVentasDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped<ISistemaDeVentasDBContext, SistemaDeVentasDBContext>();
            services.AddScoped<IClienteRepository, EFClienteRepository>();
            services.AddScoped<IProductoRepository, EFProductoRepository>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<FrmPrincipal>();

            using(var serviceScope = services.BuildServiceProvider())
            {
                var main = serviceScope.GetRequiredService<FrmPrincipal>();
                Application.Run(main);
            }
        }
    }
}
