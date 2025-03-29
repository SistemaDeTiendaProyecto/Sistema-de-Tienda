using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdPedido { get; set; }

    public int IdCliente { get; set; }

    public DateTime? FechaPago { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
