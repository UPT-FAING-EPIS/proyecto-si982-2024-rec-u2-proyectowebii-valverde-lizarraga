using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class EstadoReserva
{
    public int IdEstadoReserva { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<HistorialReserva> HistorialReservaIdEstadoActualNavigations { get; set; } = new List<HistorialReserva>();

    public virtual ICollection<HistorialReserva> HistorialReservaIdEstadoAnteriorNavigations { get; set; } = new List<HistorialReserva>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
