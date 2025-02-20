using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class HistorialReserva
{
    public int IdHistorial { get; set; }

    public int IdReserva { get; set; }

    public int IdEstudiante { get; set; }

    public int IdEstadoAnterior { get; set; }

    public int IdEstadoActual { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual EstadoReserva IdEstadoActualNavigation { get; set; } = null!;

    public virtual EstadoReserva IdEstadoAnteriorNavigation { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
