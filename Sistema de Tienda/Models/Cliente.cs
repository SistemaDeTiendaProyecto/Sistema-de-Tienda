using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? DireccionPrincipal { get; set; }

    public string? Dui { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
