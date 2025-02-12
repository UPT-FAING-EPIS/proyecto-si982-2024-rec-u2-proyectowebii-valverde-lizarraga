using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string CodigoUniversitario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ContraseñaHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? Facultad { get; set; }

    public string? Carrera { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public DateTime? UltimaConexion { get; set; }

    public virtual ICollection<HistorialNormativa> HistorialNormativas { get; set; } = new List<HistorialNormativa>();

    public virtual ICollection<Incidencia> Incidencia { get; set; } = new List<Incidencia>();

    public virtual ICollection<LogsAuditorium> LogsAuditoria { get; set; } = new List<LogsAuditorium>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
