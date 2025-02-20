using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string Nombre { get; set; } = null!;

    public int CantidadComputadoras { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int RegistradoPor { get; set; }

    public int IdEstado { get; set; }

    public virtual ICollection<DisponibilidadHorario> DisponibilidadHorarios { get; set; } = new List<DisponibilidadHorario>();

    public virtual EstadoRecurso IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual Administradore RegistradoPorNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
