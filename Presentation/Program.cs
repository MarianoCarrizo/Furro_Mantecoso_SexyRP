using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using Infraestructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            Menu menu = new(serviceProvider);
            menu.StartMenu();

        }
        static IServiceProvider CreateServiceProvider()
        {
            var collection = new ServiceCollection();

            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = configBuilder.Build();

            var connectionString = configuration.GetSection("ConnectionString").Value;


            collection.AddTransient<IClienteRepository, ClienteRepository>();
            collection.AddTransient<IOrdenRepository, OrdenRepository>();
            collection.AddTransient<IOrdenService, OrdenService>();
            collection.AddTransient<ICarritoRepository, CarritoRepository>();
            collection.AddTransient<ICarritoService, CarritoService>();
            collection.AddTransient<IProductService, ProductService>();
            collection.AddTransient<IProductRepository, ProductRepository>();
            collection.AddTransient<IClienteService, ClienteService>();
            collection.AddTransient<IVentaRepository, VentaRepository>();
            collection.AddTransient<IVentasService, VentasService>();
            collection.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

            return collection.BuildServiceProvider();

        }
    }
}