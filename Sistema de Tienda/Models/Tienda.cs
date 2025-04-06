using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Tienda.Models;

public partial class Tienda
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El Email es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "La dirección es obligatorio")]
    public string Direccion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
