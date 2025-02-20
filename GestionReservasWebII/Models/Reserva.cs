using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdEstudiante { get; set; }

    public int? IdAula { get; set; }

    public int? IdLaboratorio { get; set; }

    public int IdHorario { get; set; }

    public int NumeroBien { get; set; }

    public int IdTipoBien { get; set; }

    public DateOnly FechaReserva { get; set; }

    public int IdEstadoReserva { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<HistorialReserva> HistorialReservas { get; set; } = new List<HistorialReserva>();

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual EstadoReserva IdEstadoReservaNavigation { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual DisponibilidadHorario IdHorarioNavigation { get; set; } = null!;

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual TipoBien IdTipoBienNavigation { get; set; } = null!;
}
