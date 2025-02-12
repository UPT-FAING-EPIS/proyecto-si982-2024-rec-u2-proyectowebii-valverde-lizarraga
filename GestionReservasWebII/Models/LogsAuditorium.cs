using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class LogsAuditorium
{
    public int LogId { get; set; }

    public int UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public DateTime? FechaAccion { get; set; }

    public string? Detalles { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
