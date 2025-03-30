﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema_de_Tienda.Models;

#nullable disable

namespace Sistema_de_Tienda.Migrations
{
    [DbContext(typeof(SistemaTiendaContext))]
    partial class SistemaTiendaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sistema_de_Tienda.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id")
                        .HasName("PK__Categori__3213E83FCED191B5");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("char(64)")
                        .IsFixedLength();

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DireccionPrincipal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Dui")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("char(9)")
                        .HasColumnName("DUI")
                        .IsFixedLength();

                    b.Property<DateOnly?>("FechaNacimiento")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("char(8)")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Clientes__3213E83F26B1E013");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.DetallesPedido", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int>("Item")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id")
                        .HasName("PK__Detalles__3213E83FB41E6B6F");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProducto");

                    b.ToTable("DetallesPedidos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Factura", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("FechaFacturacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPago")
                        .HasColumnType("int");

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<string>("MetodoEnvio")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TipoFacturacion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id")
                        .HasName("PK__Facturas__3213E83F0149C359");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPago");

                    b.HasIndex("IdPedido");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pago", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("pendiente");

                    b.Property<DateTime?>("FechaPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id")
                        .HasName("PK__Pagos__3213E83F595E47F6");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPedido");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("pendiente");

                    b.Property<DateTime?>("FechaPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id")
                        .HasName("PK__Pedidos__3213E83FF2E58D98");

                    b.HasIndex("IdCliente");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<byte>("Activo")
                        .HasColumnType("tinyint")
                        .HasColumnName("activo");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdTienda")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Producto__3213E83FB9006DC5");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdTienda");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Tienda", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Logo")
                        .HasMaxLength(2000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(90)
                        .IsUnicode(false)
                        .HasColumnType("varchar(90)");

                    b.HasKey("Id")
                        .HasName("PK__Tiendas__3213E83F5F7D0BD8");

                    b.ToTable("Tiendas");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.DetallesPedido", b =>
                {
                    b.HasOne("Sistema_de_Tienda.Models.Pedido", "IdPedidoNavigation")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("IdPedido")
                        .IsRequired()
                        .HasConstraintName("FK__DetallesP__IdPed__47DBAE45");

                    b.HasOne("Sistema_de_Tienda.Models.Producto", "IdProductoNavigation")
                        .WithMany("DetallesPedidos")
                        .HasForeignKey("IdProducto")
                        .IsRequired()
                        .HasConstraintName("FK__DetallesP__IdPro__48CFD27E");

                    b.Navigation("IdPedidoNavigation");

                    b.Navigation("IdProductoNavigation");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Factura", b =>
                {
                    b.HasOne("Sistema_de_Tienda.Models.Cliente", "IdClienteNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdCliente")
                        .IsRequired()
                        .HasConstraintName("FK__Facturas__IdClie__5812160E");

                    b.HasOne("Sistema_de_Tienda.Models.Pago", "IdPagoNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdPago")
                        .IsRequired()
                        .HasConstraintName("FK__Facturas__IdPago__571DF1D5");

                    b.HasOne("Sistema_de_Tienda.Models.Pedido", "IdPedidoNavigation")
                        .WithMany("Facturas")
                        .HasForeignKey("IdPedido")
                        .IsRequired()
                        .HasConstraintName("FK__Facturas__IdPedi__5629CD9C");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdPagoNavigation");

                    b.Navigation("IdPedidoNavigation");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pago", b =>
                {
                    b.HasOne("Sistema_de_Tienda.Models.Cliente", "IdClienteNavigation")
                        .WithMany("Pagos")
                        .HasForeignKey("IdCliente")
                        .IsRequired()
                        .HasConstraintName("FK__Pagos__IdCliente__5070F446");

                    b.HasOne("Sistema_de_Tienda.Models.Pedido", "IdPedidoNavigation")
                        .WithMany("Pagos")
                        .HasForeignKey("IdPedido")
                        .IsRequired()
                        .HasConstraintName("FK__Pagos__IdPedido__4F7CD00D");

                    b.Navigation("IdClienteNavigation");

                    b.Navigation("IdPedidoNavigation");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pedido", b =>
                {
                    b.HasOne("Sistema_de_Tienda.Models.Cliente", "IdClienteNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdCliente")
                        .IsRequired()
                        .HasConstraintName("FK__Pedidos__Estado__44FF419A");

                    b.Navigation("IdClienteNavigation");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Producto", b =>
                {
                    b.HasOne("Sistema_de_Tienda.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__Productos__IdCat__3B75D760");

                    b.HasOne("Sistema_de_Tienda.Models.Tienda", "IdTiendaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdTienda")
                        .IsRequired()
                        .HasConstraintName("FK__Productos__IdTie__3C69FB99");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdTiendaNavigation");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Cliente", b =>
                {
                    b.Navigation("Facturas");

                    b.Navigation("Pagos");

                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pago", b =>
                {
                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Pedido", b =>
                {
                    b.Navigation("DetallesPedidos");

                    b.Navigation("Facturas");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Producto", b =>
                {
                    b.Navigation("DetallesPedidos");
                });

            modelBuilder.Entity("Sistema_de_Tienda.Models.Tienda", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
