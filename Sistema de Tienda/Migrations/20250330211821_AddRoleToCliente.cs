using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_Tienda.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__3213E83FCED191B5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Contrasena = table.Column<string>(type: "char(64)", unicode: false, fixedLength: true, maxLength: 64, nullable: false),
                    DireccionPrincipal = table.Column<string>(type: "text", nullable: false),
                    DUI = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefono = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__3213E83F26B1E013", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    Logo = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tiendas__3213E83F5F7D0BD8", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedidos__3213E83FF2E58D98", x => x.id);
                    table.ForeignKey(
                        name: "FK__Pedidos__Estado__44FF419A",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdTienda = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Image = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__3213E83FB9006DC5", x => x.id);
                    table.ForeignKey(
                        name: "FK__Productos__IdCat__3B75D760",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Productos__IdTie__3C69FB99",
                        column: x => x.IdTienda,
                        principalTable: "Tiendas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MetodoPago = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pagos__3213E83F595E47F6", x => x.id);
                    table.ForeignKey(
                        name: "FK__Pagos__IdCliente__5070F446",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Pagos__IdPedido__4F7CD00D",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalles__3213E83FB41E6B6F", x => x.id);
                    table.ForeignKey(
                        name: "FK__DetallesP__IdPed__47DBAE45",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__DetallesP__IdPro__48CFD27E",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdPago = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    FechaFacturacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TipoFacturacion = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MetodoEnvio = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Facturas__3213E83F0149C359", x => x.id);
                    table.ForeignKey(
                        name: "FK__Facturas__IdClie__5812160E",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Facturas__IdPago__571DF1D5",
                        column: x => x.IdPago,
                        principalTable: "Pagos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Facturas__IdPedi__5629CD9C",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_IdPedido",
                table: "DetallesPedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_IdProducto",
                table: "DetallesPedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdCliente",
                table: "Facturas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdPago",
                table: "Facturas",
                column: "IdPago");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdPedido",
                table: "Facturas",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdCliente",
                table: "Pagos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdPedido",
                table: "Pagos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdTienda",
                table: "Productos",
                column: "IdTienda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
