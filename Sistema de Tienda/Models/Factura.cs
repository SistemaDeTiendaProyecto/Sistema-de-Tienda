using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Factura
{
    public int Id { get; set; }

    public int IdPedido { get; set; }

    public int IdPago { get; set; }

    public int IdCliente { get; set; }

    public DateTime? FechaFacturacion { get; set; }

    public string TipoFacturacion { get; set; } = null!;

    public string MetodoEnvio { get; set; } = null!;

    public decimal Total { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Pago IdPagoNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
