using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Tienda.Models;

public partial class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
