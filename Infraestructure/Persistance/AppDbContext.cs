using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
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

                optionsBuilder.UseSqlServer("Server=localhost; Database=TP1; Trusted_Connection=True;");
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

                entity.Property(e => e.Dni)
                    .HasMaxLength(10)
                    .HasColumnName("DNI");

                entity.Property(e => e.Nombre).HasMaxLength(25);

                entity.Property(e => e.Telefono).HasMaxLength(13);
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

                    entity.Property(e => e.Precio).HasColumnType("decimal(15, 2)");
               });

               modelBuilder.Entity<Cliente>().HasData(
                    new Cliente { ClienteId= 1,
                         Nombre = "mateo",
                    Apellido = "carrizo",
                    Dni = "696969696",
                    Direccion = "calle siempreviva 599",
                     Telefono = "1513616310"
                    });
            modelBuilder.Entity<Producto>().HasData(
                  new Producto
                  {
                      ProductoId = 1,
                      Precio = 30,
                      Nombre = "Lata de Tomate",
                      Descripcion = "Lata de tomate de 600g",
                      Marca = "Marolio",
                      Codigo = "12956",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/5/1/5100.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 2,
                      Precio = 90,
                      Nombre = "Lata de Arvejas",
                      Descripcion = "lata de Arvejas de 600g",
                      Marca = "marolio",
                      Codigo = "77734",
                      Image = "https://supermercadoacuario.com.ar/app/files/company_35/products/66558_7797470005576.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 3,
                      Precio = 70,
                      Nombre = "Fideos Tirabuzon",
                      Descripcion = "Fideos",
                      Marca = "Marolio",
                      Codigo = "67413",
                      Image = "http://www.marolio.com.ar/sites/default/files/styles/full_post/public/Tirabuzon500g.jpg?itok=DnMf4I96",
                  },
                  new Producto
                  {
                      ProductoId = 4,
                      Precio = 148,
                      Nombre = "Cerveza  Lata ",
                      Descripcion = "500c",
                      Marca = "Brahma",
                      Codigo = "12956",
                      Image = "https://m.media-amazon.com/images/P/B006YOAB5K.01._SCLZZZZZZZ_SX500_.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 5,
                      Precio = 30,
                      Nombre = "botellla de agua con gas 500cc ",
                      Descripcion = "botella de agua con gas 500ccc",
                      Marca = "Villavicencio",
                      Codigo = "01756",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/2/2/22309.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 6,
                      Precio = 290,
                      Nombre = "Hamburgesa",
                      Descripcion = "estuche de 4 unidades",
                      Marca = "Champion",
                      Codigo = "00056",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/2/6/26762.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 7,
                      Precio = 103,
                      Nombre = "arroz",
                      Descripcion = "arroz '0000' 500gr",
                      Marca = "El Dique",
                      Codigo = "00001",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/1/7/17512.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 8,
                      Precio = 257,
                      Nombre = "Aceite",
                      Descripcion = "Aceite mezcla 900cc",
                      Marca = "Marolio",
                      Codigo = "00050",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/3/0/304.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 9,
                      Precio = 106,
                      Nombre = "Cacao en polvo",
                      Descripcion = "cacao en polvo 180gr",
                      Marca = "Marolio",
                      Codigo = "03561",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/9/8/984.jpg",
                  },
                  new Producto
                  {
                      ProductoId = 10,
                      Precio = 50,
                      Nombre = "Queso Rallado",
                      Descripcion = "queso rallado 100Gr",
                      Marca = "Gikar",
                      Codigo = "09993",
                      Image = "https://maxiconsumo.com/media/catalog/product/cache/8313a15b471f948db4d9d07d4a9f04a2/1/7/17076.jpg",
                  });

        }

    }









}



