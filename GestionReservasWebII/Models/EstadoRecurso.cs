using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;

public partial class EstadoRecurso
{
    public int IdEstadoRecurso { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Aula> Aulas { get; set; } = new List<Aula>();

    public virtual ICollection<Laboratorio> Laboratorios { get; set; } = new List<Laboratorio>();
}
