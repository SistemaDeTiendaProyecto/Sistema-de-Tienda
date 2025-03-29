using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class DetallesPedido
{
    public int Id { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    public int Item { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
