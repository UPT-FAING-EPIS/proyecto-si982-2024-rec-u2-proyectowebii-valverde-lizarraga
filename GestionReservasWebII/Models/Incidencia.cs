using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;
//ejemplos
public partial class Incidencia
{
    public int IncidenciaId { get; set; }

    public int RecursoId { get; set; }

    public int UsuarioId { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime? FechaReporte { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaResolucion { get; set; }

    public virtual Recurso Recurso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
