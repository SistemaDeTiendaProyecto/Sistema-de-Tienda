using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Tienda.Models;

public partial class SistemaTiendaContext : DbContext
{
    public SistemaTiendaContext()
    {
    }

    public SistemaTiendaContext(DbContextOptions<SistemaTiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Tienda> Tiendas { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83FCED191B5");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3213E83F26B1E013");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DireccionPrincipal).HasColumnType("text");
            entity.Property(e => e.Dui)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DUI");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3213E83FB41E6B6F");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesP__IdPed__47DBAE45");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesP__IdPro__48CFD27E");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3213E83F0149C359");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FechaFacturacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoEnvio)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TipoFacturacion)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__IdClie__5812160E");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__IdPago__571DF1D5");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__IdPedi__5629CD9C");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3213E83F595E47F6");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("pendiente");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__IdCliente__5070F446");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__IdPedido__4F7CD00D");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedidos__3213E83FF2E58D98");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("pendiente");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos__Estado__44FF419A");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83FB9006DC5");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IdCat__3B75D760");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTienda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IdTie__3C69FB99");
        });

        modelBuilder.Entity<Tienda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tiendas__3213E83F5F7D0BD8");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
