using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string CodigoUniversitario { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<HistorialReserva> HistorialReservas { get; set; } = new List<HistorialReserva>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
