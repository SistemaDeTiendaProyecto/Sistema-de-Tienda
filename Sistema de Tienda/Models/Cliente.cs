using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Tienda.Models;

public partial class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
    public string Correo { get; set; } = null!;

    [StringLength(50, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 50 caracteres.")]
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = null!;

    [Required(ErrorMessage = "La Direccion es obligatoria")]
    public string? DireccionPrincipal { get; set; }

    [Required(ErrorMessage = "El DUI es obligatorio")]
    public string? Dui { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    public DateOnly? FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El Numero es obligatorio")]
    [StringLength(8, ErrorMessage = "Solo se permiten 8 digitos")]
    [RegularExpression(@"^\d+(\d{1,2})?$", ErrorMessage = "Solo se permiten Numeros")]
    public string? Telefono { get; set; }

    public DateTime? FechaRegistro { get; set; }
    [Required(ErrorMessage = "El Rol es obligatorio")]
    public string Role { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
