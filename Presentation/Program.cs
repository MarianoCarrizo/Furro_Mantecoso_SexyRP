using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using Infraestructure.Persistance;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Presentation
{
    internal class Program
    {
          static void Main(string[] args)
          {
               var builder = WebApplication.CreateBuilder(args);
               var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
               IConfiguration configuration = configBuilder.Build();
               string connectionString = configuration.GetSection("ConnectionString").Value;

               // Add services to the container.

               builder.Services.AddControllers();
               // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
               builder.Services.AddEndpointsApiExplorer();
               builder.Services.AddSwaggerGen();
               builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
               builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
               builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
               builder.Services.AddTransient<IVentaRepository, VentaRepository>();
               builder.Services.AddTransient<IClienteService, ClienteService>();
               builder.Services.AddTransient<IOrdenService, OrdenService>();
               builder.Services.AddTransient<IVentaService, VentaService>();
               builder.Services.AddAutoMapper(typeof(Program));

               var app = builder.Build();

               // Configure the HTTP request pipeline.
               if (app.Environment.IsDevelopment())
               {
                    app.UseSwagger();
                    app.UseSwaggerUI();
               }

               app.UseHttpsRedirection();

               app.UseAuthorization();


               app.MapControllers();

               app.Run();
          }
    }
}