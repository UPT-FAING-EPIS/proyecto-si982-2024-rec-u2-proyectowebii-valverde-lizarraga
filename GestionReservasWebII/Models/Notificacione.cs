using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Notificacione
{
    public int NotificacionId { get; set; }

    public int UsuarioId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public DateTime? FechaEnvio { get; set; }

    public bool Leido { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
