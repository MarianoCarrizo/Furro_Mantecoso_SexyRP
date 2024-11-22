using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrito", x => x.CarritoId);
                    table.ForeignKey(
                        name: "FK_Carrito_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoProducto",
                columns: table => new
                {
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoProducto", x => new { x.CarritoId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_CarritoProducto_Carrito_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carrito",
                        principalColumn: "CarritoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    OrdenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.OrdenId);
                    table.ForeignKey(
                        name: "FK_Orden_Carrito_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carrito",
                        principalColumn: "CarritoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "ClienteId", "Apellido", "Direccion", "DNI", "Mail", "Nombre", "Password", "Telefono", "UserName" },
                values: new object[,]
                {
                    { 1, "Carrizo", "calle siempreviva 599", "41389372", "mariano.carrizo4280@gmail.com", "Mariano", "42804254M@c!", "1126824939", "myuubi" },
                    { 2, "Veliz", "calle siempreviva 599", "41354119", "warlockphantasy@gmail.com", "Celeste", "42804254M@c!", "1513616310", "celenya" },
                    { 3, "simpson", "calle siempreviva 599", "31354119", "donbarredora@gmail.com", "homero", "mmmCerveza!", "1513616317", "donbarredora" }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "ProductoId", "Categoria", "Codigo", "Descripcion", "Image", "Marca", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "Almacen", "12956", "Lata de tomate de 600g", "https://i.imgur.com/gqJVlWk.jpg", "Marolio", "Lata de Tomate", 800m },
                    { 2, "Almacen", "77734", "lata de Arvejas de 600g", "https://i.imgur.com/AARTUJY.jpg", "marolio", "Lata de Arvejas", 1000m },
                    { 3, "Almacen", "67413", "Fideos", "https://i.imgur.com/HNZyMIF.jpg", "Marolio", "Fideos Tirabuzon", 1700m },
                    { 4, "Almacen", "12956", "500c", "https://i.imgur.com/lUrXZUw.jpg", "Brahma", "Cerveza  Lata ", 1500m },
                    { 5, "Almacen", "01756", "botella de agua con gas 500ccc", "https://i.imgur.com/CfHIPke.jpg", "Villavicencio", "botellla de agua con gas 500cc ", 800m },
                    { 6, "Almacen", "00056", "estuche de 4 unidades", "https://i.imgur.com/doMqjA2.jpg", "Champion", "Hamburgesa", 3500m },
                    { 7, "Almacen", "00001", "arroz '0000' 500gr", "https://i.imgur.com/k5enq60.jpg", "El Dique", "arroz", 800m },
                    { 8, "Almacen", "00050", "Aceite mezcla 900cc", "https://i.imgur.com/YZJQGpC.jpg", "Marolio", "Aceite", 1500m },
                    { 9, "Almacen", "03561", "cacao en polvo 180gr", "https://i.imgur.com/eAFHPvX.jpg", "Marolio", "Cacao en polvo", 1200m },
                    { 10, "Almacen", "09993", "queso rallado 100Gr", "https://i.imgur.com/jaFvFal.jpg", "Gikar", "Queso Rallado", 300m },
                    { 11, "Limpieza", "09995", "Desinfectante de Superficies 380ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795754.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Cif", "Limpiador Líquido", 2634m },
                    { 12, "Limpieza", "09970", "Aerosol Desinfectante de Ambientes y Superficies 360cm3", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795778.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Cif", "Aerosol Desinfectante de Ambientes y Superficies", 3631m },
                    { 13, "Limpieza", "09971", "Limpiador Desinfectante de Superficies 250ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795808.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Cif", "Limpiador Desinfectante de Superficies Cif Gel", 3631m },
                    { 14, "Limpieza", "09972", "Limpiador Desinfectante Para Pisos 750ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795815.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Cif", "Limpiador Desinfectante Para Pisos", 3634m },
                    { 15, "Limpieza", "09973", "Pala Click Plastica Residuos", "https://clubdebeneficios.com/media/catalog/product/p/a/palaclick.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Virulana", "Pala Click Plastica", 4620m },
                    { 16, "Limpieza", "09974", "Con Espuma Y Secador", "https://clubdebeneficios.com/media/catalog/product/n/o/noso0964.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Virulana", "Limpiavidrios", 4634m },
                    { 17, "Limpieza", "09975", "esponja de bronce 16gr", "https://clubdebeneficios.com/media/catalog/product/b/r/bronce_alta_ean_7794440002238.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Virulana", "Virulana Bronce", 800m },
                    { 18, "Limpieza", "09976", "rollitos 70gr", "https://clubdebeneficios.com/media/catalog/product/r/o/rollitosx10_alta_ean_7794440101702.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Virulana", "Virulana Rollitos X 10", 1400m },
                    { 19, "Limpieza", "09977", "Vim Citrus 700ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290794979.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Vim", "Lavandina en Gel", 1900m },
                    { 20, "Limpieza", "09978", "pino 100gr", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290794153.h.png?quality=80&fit=bounds&height=&width=&canvas=:", "Vim", "Pastilla Para Inodoro", 1600m },
                    { 21, "Limpieza", "09979", "Desengrasante 1kg", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290792470.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Sun", "Polvo para máquinas Lavavajilla", 1300m },
                    { 22, "Perfumeria", "09980", "Afeitadora Soleil Clic Sensitive 50gr", "https://clubdebeneficios.com/media/catalog/product/m/o/mockup_soleil_clic_sensitive_bl5_2022_frente.png?quality=80&fit=bounds&height=&width=&canvas=:", "Bic", "Afeitadora Soleil Clic Sensitive", 5600m },
                    { 23, "Perfumeria", "09981", "Afeitadora Flex 3 Hybrid 45gr", "https://clubdebeneficios.com/media/catalog/product/9/6/968722_flex3_hybrid_5un.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Bic", "Afeitadora Flex 3 Hybrid", 4500m },
                    { 24, "Perfumeria", "09982", "Sérum Corporal Dove Pro-Retinol 400ml", "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252882_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Dove", "Sérum Corporal Dove Pro-Retinol", 5600m },
                    { 25, "Perfumeria", "09983", "Sérum Corporal Dove Niacinamida 400ml", "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252875_1_1.jpg?quality=80&fit=bounds&height=700&width=700&canvas=700:700", "Dove", "Sérum Corporal Dove Niacinamida", 3600m },
                    { 26, "Perfumeria", "09984", "Sérum Corporal Dove Pro-Ceramidas 400ml", "https://clubdebeneficios.com/media/catalog/product/7/5/7506306252868_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Dove", "Sérum Corporal Dove Pro-Ceramidas", 3600m },
                    { 27, "Perfumeria", "09985", "Antitranspirante Dove Men+Care Extra fresh Roll-On 50ml", "https://clubdebeneficios.com/media/catalog/product/7/8/78925519.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Dove", "Antitranspirante Dove Men+Care Extra fresh Roll-On", 3500m },
                    { 28, "Perfumeria", "09986", "Desodorante Aerosol Axe BZRP 150ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791293051031_1_6.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Axe", "Desodorante Aerosol Axe BZRP", 2100m },
                    { 29, "Perfumeria", "09987", "Multipack 90gr", "https://clubdebeneficios.com/media/catalog/product/7/8/7891150095700_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Dove", "Jabón en Barra Dove Piel Sensible x3", 2300m },
                    { 30, "Perfumeria", "09988", "Jabón Liquido para manos Lux Botanicals Orquídea Negra 250ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791293048833_1_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Lux", "Jabón Liquido para manos Lux Botanicals Orquídea Negra", 2500m },
                    { 31, "Perfumeria", "09989", "Multipack 120gr", "https://clubdebeneficios.com/media/catalog/product/7/7/7791293050959.h_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Rexona", "Jabón en Barra Rexona Nutritive Orchid x3", 2300m },
                    { 32, "Perfumeria", "09990", "Multipack 120gr", "https://clubdebeneficios.com/media/catalog/product/7/7/7791293050935.h.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Rexona", "Jabón en Barra Rexona Cotton Fresh x3", 2300m },
                    { 33, "CuidadoRopa", "09991", "Suavizante Concentrado Comfort Energia Floral 1lt", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795952.jpg?quality=80&fit=bounds&height=700&width=700&canvas=700:700", "Comfort", "Suavizante Concentrado Comfort Energia Floral 1lt", 6555m },
                    { 35, "Cuidado de ropa", "09993", "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795495.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Ala", "Jabón Líquido Concentrado para Diluir Ala con Bicarbonato 500ml", 6479m },
                    { 36, "Cuidado de ropa", "09960", "Jabón Líquido Concentrado para Diluir Skip Power Deluxe 500ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795518.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Skip", "Jabón Líquido Concentrado para Diluir Skip Power Deluxe 500ml", 30479m },
                    { 37, "Cuidado de ropa", "09961", "Jabón Líquido Concentrado para Diluir Skip Power Oxi 500ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290795501.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Skip", "Jabón Líquido Concentrado para Diluir Skip Power Oxi 500ml", 30479m },
                    { 38, "Cuidado de ropa", "09962", "Suavizante para ropa Vivere Explosión Floral Clásico DP 3L", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793743_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Vivere", "Suavizante para ropa Vivere Explosión Floral Clásico DP 3L", 15000m },
                    { 39, "Cuidado de ropa", "09963", "Suavizantes para ropa Vivere Explosión Floral Plancha Fácil 810 ml", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793774.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Vivere", "Suavizantes para ropa Vivere Explosión Floral Plancha Fácil 810 ml", 6418m },
                    { 40, "Cuidado de ropa", "09964", "Saltar al comienzo de la galería de imágenes\r\nSuavizante para ropa Vivere Explosión Floral Clásico 3 lt", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793866_1.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Vivere", "Suavizante para ropa Vivere Explosión Floral Clásico 3 lt", 8418m }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "ProductoId", "Categoria", "Codigo", "Descripcion", "Image", "Marca", "Nombre", "Precio" },
                values: new object[] { 41, "Cuidado de ropa", "09965", "Suavizante Concentrado Comfort Frescor Intenso 500 ml", "https://clubdebeneficios.com/media/catalog/product/7/8/7891150000971_2.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Comfort", "Suavizante Concentrado Comfort Frescor Intenso 500 ml", 4959m });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "ProductoId", "Categoria", "Codigo", "Descripcion", "Image", "Marca", "Nombre", "Precio" },
                values: new object[] { 42, "Cuidado de ropa", "09966", "Suavizante Regular Comfort Clásico Bidón 5lt", "https://clubdebeneficios.com/media/catalog/product/7/7/7791290793705.h.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Comfort", "Suavizante Regular Comfort Clásico Bidón 5lt", 10845m });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "ProductoId", "Categoria", "Codigo", "Descripcion", "Image", "Marca", "Nombre", "Precio" },
                values: new object[] { 6479, "Cuidado de ropa", "09992", "Suavizante Concentrado Comfort Energia Floral 500ml", "https://clubdebeneficios.com/media/catalog/product/7/8/7891150025288.jpg?quality=80&fit=bounds&height=&width=&canvas=:", "Comfort", "Suavizante Concentrado Comfort Energia Floral 500ml", 50m });

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_ClienteId",
                table: "Carrito",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProducto_ProductoId",
                table: "CarritoProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_DNI",
                table: "Cliente",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Mail",
                table: "Cliente",
                column: "Mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Telefono",
                table: "Cliente",
                column: "Telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_UserName",
                table: "Cliente",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orden_CarritoId",
                table: "Orden",
                column: "CarritoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoProducto");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Carrito");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
