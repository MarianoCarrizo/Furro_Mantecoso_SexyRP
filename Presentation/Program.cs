﻿using Application.DataAccess;
using Application.Services;
using Application.Services.Interfaces;
using Infraestructure.Persistance;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

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



            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
            builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<ICarritoRepository, CarritoRepository>();
            builder.Services.AddTransient<ICarritoService, CarritoService>();
            builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<IOrdenService, OrdenService>();
            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = "BACKEND";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.MapControllers();

            app.Run();
        }
    }
}