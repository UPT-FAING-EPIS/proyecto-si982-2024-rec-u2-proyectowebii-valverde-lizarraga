using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class Incidencia
{
    public int IdIncidencia { get; set; }

    public int IdEstudiante { get; set; }

    public int? IdAula { get; set; }

    public int? IdLaboratorio { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdEstadoIncidencia { get; set; }

    public DateTime? FechaReporte { get; set; }

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual EstadoIncidencium IdEstadoIncidenciaNavigation { get; set; } = null!;

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }
}
