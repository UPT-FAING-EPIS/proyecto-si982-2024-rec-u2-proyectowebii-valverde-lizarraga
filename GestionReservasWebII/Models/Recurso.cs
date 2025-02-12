using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Recurso
{
    public int RecursoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string? Caracteristicas { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
