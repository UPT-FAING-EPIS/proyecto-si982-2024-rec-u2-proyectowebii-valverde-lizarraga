using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Reserva
{
    public int ReservaId { get; set; }

    public int UsuarioId { get; set; }

    public int RecursoId { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateTime HoraInicio { get; set; }

    public DateTime HoraFin { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    public virtual Recurso Recurso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
