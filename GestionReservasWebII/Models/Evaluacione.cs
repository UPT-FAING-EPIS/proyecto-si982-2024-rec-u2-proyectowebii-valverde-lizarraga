using System;
using System.Collections.Generic;

namespace GestionReservasWebII.Models;
//asdsasadsadsacsacsacssadsaadasdsa
public partial class Evaluacione
{
    public int EvaluacionId { get; set; }

    public int ReservaId { get; set; }

    public int UsuarioId { get; set; }

    public int Puntuacion { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaEvaluacion { get; set; }

    public virtual Reserva Reserva { get; set; } = null!;
}
