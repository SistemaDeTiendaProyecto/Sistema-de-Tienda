using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Producto
{
    public int Id { get; set; }

    public int IdCategoria { get; set; }

    public int IdTienda { get; set; }

    public string Nombre { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public byte Activo { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Tienda IdTiendaNavigation { get; set; } = null!;
}
