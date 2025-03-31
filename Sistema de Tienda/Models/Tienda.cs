using System;
using System.Collections.Generic;

namespace Sistema_de_Tienda.Models;

public partial class Tienda
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;


    public string? Email { get; set; }

    public string Direccion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
