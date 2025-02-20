using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class EstadoIncidencium
{
    public int IdEstadoIncidencia { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();
}
