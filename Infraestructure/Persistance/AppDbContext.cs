using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Carrito> Carritos { get; set; } = null!;
        public virtual DbSet<CarritoProducto> CarritoProductos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Orden> Ordenes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=localhost; Database=TP2; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable("Carrito");

                entity.Property(e => e.CarritoId).ValueGeneratedNever();

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.ClienteId);

            });

            modelBuilder.Entity<CarritoProducto>(entity =>
            {
                entity.HasKey(e => new { e.CarritoId, e.ProductoId });

                entity.ToTable("CarritoProducto");

                entity.HasOne(d => d.Carrito)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.CarritoId);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.ProductoId);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Apellido).HasMaxLength(25);

                entity.Property(e => e.UserName).HasMaxLength(25);
                entity.HasIndex(e => e.UserName).IsUnique();

                entity.Property(e => e.Mail).HasMaxLength(70);
                entity.HasIndex(e => e.Mail).IsUnique();

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.Dni)
                    .HasMaxLength(10)
                    .HasColumnName("DNI");

                entity.HasIndex(e => e.Dni).IsUnique();

                entity.Property(e => e.Nombre).HasMaxLength(25);

                entity.Property(e => e.Telefono).HasMaxLength(13);
                entity.HasIndex(e => e.Telefono).IsUnique();
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("Orden");

                entity.HasIndex(e => e.CarritoId)
                    .IsUnique();

                entity.Property(e => e.OrdenId).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Carrito)
                    .WithOne(p => p.Orden)
                    .HasForeignKey<Orden>(d => d.CarritoId);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.ProductoId);

                entity.Property(e => e.Codigo).HasMaxLength(25);

                entity.Property(e => e.Marca).HasMaxLength(25);

                entity.Property(e => e.Categoria).HasMaxLength(25);

                entity.Property(e => e.Precio).HasColumnType("decimal(15, 2)");
            });

            modelBuilder.Entity<Cliente>().HasData(
                 new Cliente
                 {
                     ClienteId = 1,
                     Nombre = "Mariano",
                     Apellido = "Carrizo",
                     UserName = "myuubi",
                     Mail = "mariano.carrizo4280@gmail.com",
                     Password = "42804254M@c!",
                     Dni = "41389372",
                     Direccion = "calle siempreviva 599",
                     Telefono = "1126824939"
                 }, new Cliente
                 {
                     ClienteId = 2,
                     Nombre = "Celeste",
                     Apellido = "Veliz",
                     UserName = "celenya",
                     Mail = "warlockphantasy@gmail.com",
                     Password = "42804254M@c!",
                     Dni = "41354119",
                     Direccion = "calle siempreviva 599",
                     Telefono = "1513616310"
                 },
                 new Cliente
                 {
                     ClienteId = 3,
                     Nombre = "homero",
                     Apellido = "simpson",
                     UserName = "donbarredora",
                     Mail = "donbarredora@gmail.com",
                     Password = "mmmCerveza!",
                     Dni = "31354119",
                     Direccion = "calle siempreviva 599",
                     Telefono = "1513616317"
                 });
            modelBuilder.Entity<Producto>().HasData(
                  new Producto
                  {
                      ProductoId = 1,
                      Precio = 800,
                      Nombre = "Lata de Tomate",
                      Descripcion = "Lata de tomate de 600g",
                      Categoria = "Almacen",
                      Marca = "Marolio",
                      Codigo = "12956",
                      Image = "https://i.imgur.com/gqJVlWk.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 2,
                      Precio = 1000,
                      Nombre = "Lata de Arvejas",
                      Descripcion = "lata de Arvejas de 600g",
                      Categoria = "Almacen",
                      Marca = "marolio",
                      Codigo = "77734",
                      Image = "https://i.imgur.com/AARTUJY.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 3,
                      Precio = 1700,
                      Nombre = "Fideos Tirabuzon",
                      Descripcion = "Fideos",
                      Marca = "Marolio",
                      Categoria = "Almacen",
                      Codigo = "67413",
                      Image = "https://i.imgur.com/HNZyMIF.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 4,
                      Precio = 1500,
                      Nombre = "Cerveza  Lata ",
                      Descripcion = "500c",
                      Categoria = "Almacen",
                      Marca = "Brahma",
                      Codigo = "12956",
                      Image = "https://i.imgur.com/lUrXZUw.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 5,
                      Precio = 800,
                      Nombre = "botellla de agua con gas 500cc ",
                      Descripcion = "botella de agua con gas 500ccc",
                      Categoria = "Almacen",
                      Marca = "Villavicencio",
                      Codigo = "01756",
                      Image = "https://i.imgur.com/CfHIPke.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 6,
                      Precio = 3500,
                      Nombre = "Hamburgesa",
                      Descripcion = "estuche de 4 unidades",
                      Categoria = "Almacen",
                      Marca = "Champion",
                      Codigo = "00056",
                      Image = "https://i.imgur.com/doMqjA2.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 7,
                      Precio = 800,
                      Nombre = "arroz",
                      Categoria = "Almacen",
                      Descripcion = "arroz '0000' 500gr",
                      Marca = "El Dique",
                      Codigo = "00001",
                      Image = "https://i.imgur.com/k5enq60.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 8,
                      Precio = 1500,
                      Nombre = "Aceite",
                      Descripcion = "Aceite mezcla 900cc",
                      Marca = "Marolio",
                      Categoria = "Almacen",
                      Codigo = "00050",
                      Image = "https://i.imgur.com/YZJQGpC.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 9,
                      Precio = 1200,
                      Nombre = "Cacao en polvo",
                      Descripcion = "cacao en polvo 180gr",
                      Categoria = "Almacen",
                      Marca = "Marolio",
                      Codigo = "03561",
                      Image = "https://i.imgur.com/eAFHPvX.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 10,
                      Precio = 300,
                      Nombre = "Queso Rallado",
                      Descripcion = "queso rallado 100Gr",
                      Categoria = "Almacen",
                      Marca = "Gikar",
                      Codigo = "09993",
                      Image = "https://i.imgur.com/jaFvFal.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 11,
                      Precio = 2634,
                      Nombre = "Limpiador Líquido",
                      Descripcion = "Desinfectante de Superficies 380ml",
                      Categoria = "Limpieza",
                      Marca = "Cif",
                      Codigo = "09995",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795754.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 12,
                      Precio = 3631,
                      Nombre = "Aerosol Desinfectante de Ambientes y Superficies",
                      Descripcion = "Aerosol Desinfectante de Ambientes y Superficies 360cm3",
                      Categoria = "Limpieza",
                      Marca = "Cif",
                      Codigo = "09970",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795778.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 13,
                      Precio = 3631,
                      Nombre = "Limpiador Desinfectante de Superficies Cif Gel",
                      Descripcion = "Limpiador Desinfectante de Superficies 250ml",
                      Categoria = "Limpieza",
                      Marca = "Cif",
                      Codigo = "09971",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795808.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 14,
                      Precio = 3634,
                      Nombre = "Limpiador Desinfectante Para Pisos",
                      Descripcion = "Limpiador Desinfectante Para Pisos 750ml",
                      Categoria = "Limpieza",
                      Marca = "Cif",
                      Codigo = "09972",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795815.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 15,
                      Precio = 4620,
                      Nombre = "Pala Click Plastica",
                      Descripcion = "Pala Click Plastica Residuos",
                      Categoria = "Limpieza",
                      Marca = "Virulana",
                      Codigo = "09973",
                      Image = "https://clubdebeneficios.com/media/catalog/product/p/a/palaclick.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 16,
                      Precio = 4634,
                      Nombre = "Limpiavidrios",
                      Descripcion = "Con Espuma Y Secador",
                      Categoria = "Limpieza",
                      Marca = "Virulana",
                      Codigo = "09974",
                      Image = "https://clubdebeneficios.com/media/catalog/product/n/o/noso0964.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 17,
                      Precio = 800,
                      Nombre = "Virulana Bronce",
                      Descripcion = "esponja de bronce 16gr",
                      Categoria = "Limpieza",
                      Marca = "Virulana",
                      Codigo = "09975",
                      Image = "https://clubdebeneficios.com/media/catalog/product/b/r/bronce_alta_ean_7794440002238.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 18,
                      Precio = 1400,
                      Nombre = "Virulana Rollitos X 10",
                      Descripcion = "rollitos 70gr",
                      Categoria = "Limpieza",
                      Marca = "Virulana",
                      Codigo = "09976",
                      Image = "https://clubdebeneficios.com/media/catalog/product/r/o/rollitosx10_alta_ean_7794440101702.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 19,
                      Precio = 1900,
                      Nombre = "Lavandina en Gel",
                      Descripcion = "Vim Citrus 700ml",
                      Categoria = "Limpieza",
                      Marca = "Vim",
                      Codigo = "09977",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290794979.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 20,
                      Precio = 1600,
                      Nombre = "Pastilla Para Inodoro",
                      Descripcion = "pino 100gr",
                      Categoria = "Limpieza",
                      Marca = "Vim",
                      Codigo = "09978",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290794153.h.png?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 21,
                      Precio = 1300,
                      Nombre = "Polvo para máquinas Lavavajilla",
                      Descripcion = "Desengrasante 1kg",
                      Categoria = "Limpieza",
                      Marca = "Sun",
                      Codigo = "09979",
                      Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290792470.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                  new Producto
                  {
                      ProductoId = 22,
                      Precio = 5600,
                      Nombre = "Afeitadora Soleil Clic Sensitive",
                      Descripcion = "Afeitadora Soleil Clic Sensitive 50gr",
                      Categoria = "Perfumeria",
                      Marca = "Bic",
                      Codigo = "09980",
                      Image = "https://clubdebeneficios.com/media/catalog/product/m/o/mockup_soleil_clic_sensitive_bl5_2022_frente.png?quality=80&fit=bounds&height=&width=&canvas=:",
                  },
                     new Producto
                     {
                         ProductoId = 23,
                         Precio = 4500,
                         Nombre = "Afeitadora Flex 3 Hybrid",
                         Descripcion = "Afeitadora Flex 3 Hybrid 45gr",
                         Categoria = "Perfumeria",
                         Marca = "Bic",
                         Codigo = "09981",
                         Image = "https://clubdebeneficios.com/media/catalog/product/9/6/968722_flex3_hybrid_5un.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                     },
                        new Producto
                        {
                            ProductoId = 24,
                            Precio = 5600,
                            Nombre = "Sérum Corporal Dove Pro-Retinol",
                            Descripcion = "Sérum Corporal Dove Pro-Retinol 400ml",
                            Categoria = "Perfumeria",
                            Marca = "Dove",
                            Codigo = "09982",
                            Image = "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252882_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                        },
                           new Producto
                           {
                               ProductoId = 25,
                               Precio = 3600,
                               Nombre = "Sérum Corporal Dove Niacinamida",
                               Descripcion = "Sérum Corporal Dove Niacinamida 400ml",
                               Categoria = "Perfumeria",
                               Marca = "Dove",
                               Codigo = "09983",
                               Image = "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252875_1_1.jpg?quality=80&fit=bounds&height=700&width=700&canvas=700:700",
                           },
                              new Producto
                              {
                                  ProductoId = 26,
                                  Precio = 3600,
                                  Nombre = "Sérum Corporal Dove Pro-Ceramidas",
                                  Descripcion = "Sérum Corporal Dove Pro-Ceramidas 400ml",
                                  Categoria = "Perfumeria",
                                  Marca = "Dove",
                                  Codigo = "09984",
                                  Image = "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252868_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                              },
                                 new Producto
                                 {
                                     ProductoId = 27,
                                     Precio = 3500,
                                     Nombre = "Antitranspirante Dove Men+Care Extra fresh Roll-On",
                                     Descripcion = "Antitranspirante Dove Men+Care Extra fresh Roll-On 50ml",
                                     Categoria = "Perfumeria",
                                     Marca = "Dove",
                                     Codigo = "09985",
                                     Image = "https://clubdebeneficios.com/media/catalog/product/7/8/78925519.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                 },
                                    new Producto
                                    {
                                        ProductoId = 28,
                                        Precio = 2100,
                                        Nombre = "Desodorante Aerosol Axe BZRP",
                                        Descripcion = "Desodorante Aerosol Axe BZRP 150ml",
                                        Categoria = "Perfumeria",
                                        Marca = "Axe",
                                        Codigo = "09986",
                                        Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791293051031_1_6.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                    },
                                       new Producto
                                       {
                                           ProductoId = 29,
                                           Precio = 2300,
                                           Nombre = "Jabón en Barra Dove Piel Sensible x3",
                                           Descripcion = "Multipack 90gr",
                                           Categoria = "Perfumeria",
                                           Marca = "Dove",
                                           Codigo = "09987",
                                           Image = "https://clubdebeneficios.com/media/catalog/product/7/8/7891150095700_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                       },
                                          new Producto
                                          {
                                              ProductoId = 30,
                                              Precio = 2500,
                                              Nombre = "Jabón Liquido para manos Lux Botanicals Orquídea Negra",
                                              Descripcion = "Jabón Liquido para manos Lux Botanicals Orquídea Negra 250ml",
                                              Categoria = "Perfumeria",
                                              Marca = "Lux",
                                              Codigo = "09988",
                                              Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791293048833_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                          },
                                             new Producto
                                             {
                                                 ProductoId = 31,
                                                 Precio = 2300,
                                                 Nombre = "Jabón en Barra Rexona Nutritive Orchid x3",
                                                 Descripcion = "Multipack 120gr",
                                                 Categoria = "Perfumeria",
                                                 Marca = "Rexona",
                                                 Codigo = "09989",
                                                 Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791293050959.h_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                             }, new Producto
                                             {
                                                 ProductoId = 32,
                                                 Precio = 2300,
                                                 Nombre = "Jabón en Barra Rexona Cotton Fresh x3",
                                                 Descripcion = "Multipack 120gr",
                                                 Categoria = "Perfumeria",
                                                 Marca = "Rexona",
                                                 Codigo = "09990",
                                                 Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791293050935.h.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
                                             }, new Producto
                                             {
                                                 ProductoId = 33,
                                                 Precio = 6555,
                                                 Nombre = "Suavizante Concentrado Comfort Energia Floral 1lt",
                                                 Descripcion = "Suavizante Concentrado Comfort Energia Floral 1lt",
                                                 Categoria = "CuidadoRopa",
                                                 Marca = "Comfort",
                                                 Codigo = "09991",
                                                 Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795952.jpg?quality=80&fit=bounds&height=700&width=700&canvas=700:700",
                                             },
              new Producto
              {
                  ProductoId = 6479,
                  Precio = 50,
                  Nombre = "Suavizante Concentrado Comfort Energia Floral 500ml",
                  Descripcion = "Suavizante Concentrado Comfort Energia Floral 500ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Comfort",
                  Codigo = "09992",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/8/7891150025288.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 35,
                  Precio = 6479,
                  Nombre = "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500ml",
                  Descripcion = "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Ala",
                  Codigo = "09993",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795495.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 36,
                  Precio = 30479,
                  Nombre = "Jabón Líquido Concentrado para Diluir Skip Power Deluxe 500ml",
                  Descripcion = "Jabón Líquido Concentrado para Diluir Skip Power Deluxe 500ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Skip",
                  Codigo = "09960",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795518.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 37,
                  Precio = 30479,
                  Nombre = "Jabón Líquido Concentrado para Diluir Skip Power Oxi 500ml",
                  Descripcion = "Jabón Líquido Concentrado para Diluir Skip Power Oxi 500ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Skip",
                  Codigo = "09961",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795501.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 38,
                  Precio = 15000,
                  Nombre = "Suavizante para ropa Vivere Explosión Floral Clásico DP 3L",
                  Descripcion = "Suavizante para ropa Vivere Explosión Floral Clásico DP 3L",
                  Categoria = "Cuidado de ropa",
                  Marca = "Vivere",
                  Codigo = "09962",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793743_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 39,
                  Precio = 6418,
                  Nombre = "Suavizantes para ropa Vivere Explosión Floral Plancha Fácil 810 ml",
                  Descripcion = "Suavizantes para ropa Vivere Explosión Floral Plancha Fácil 810 ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Vivere",
                  Codigo = "09963",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793774.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 40,
                  Precio = 8418,
                  Nombre = "Suavizante para ropa Vivere Explosión Floral Clásico 3 lt",
                  Descripcion = "Saltar al comienzo de la galería de imágenes\r\nSuavizante para ropa Vivere Explosión Floral Clásico 3 lt",
                  Categoria = "Cuidado de ropa",
                  Marca = "Vivere",
                  Codigo = "09964",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793866_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 41,
                  Precio = 4959,
                  Nombre = "Suavizante Concentrado Comfort Frescor Intenso 500 ml",
                  Descripcion = "Suavizante Concentrado Comfort Frescor Intenso 500 ml",
                  Categoria = "Cuidado de ropa",
                  Marca = "Comfort",
                  Codigo = "09965",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/8/7891150000971_2.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              }, new Producto
              {
                  ProductoId = 42,
                  Precio = 10845,
                  Nombre = "Suavizante Regular Comfort Clásico Bidón 5lt",
                  Descripcion = "Suavizante Regular Comfort Clásico Bidón 5lt",
                  Categoria = "Cuidado de ropa",
                  Marca = "Comfort",
                  Codigo = "09966",
                  Image = "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793705.h.jpg?quality=80&fit=bounds&height=&width=&canvas=:",
              });
            

        }

    }









}



