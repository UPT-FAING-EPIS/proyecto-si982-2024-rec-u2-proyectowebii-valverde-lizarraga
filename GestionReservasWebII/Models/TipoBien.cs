using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class TipoBien
{
    public int IdTipoBien { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
