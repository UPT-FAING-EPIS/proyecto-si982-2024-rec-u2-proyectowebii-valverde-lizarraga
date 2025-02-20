using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Docente
{
    public int IdDocente { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool? Estado { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;
}
