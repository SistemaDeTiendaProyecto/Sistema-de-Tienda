using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public DateTime? FechaPedido { get; set; }

    public decimal Total { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
