using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class DisponibilidadHorario
{
    public int IdHorario { get; set; }

    public int? IdAula { get; set; }

    public int? IdLaboratorio { get; set; }

    public TimeOnly? HoraInicio { get; set; }

    public TimeOnly? HoraFin { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public static implicit operator DisponibilidadHorario(DisponibilidadHorario v)
    {
        throw new NotImplementedException();
    }
}
