using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Administradore
{
    public int IdAdministrador { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Aula> Aulas { get; set; } = new List<Aula>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Laboratorio> Laboratorios { get; set; } = new List<Laboratorio>();
}
