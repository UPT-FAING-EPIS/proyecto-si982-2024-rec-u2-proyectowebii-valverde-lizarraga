using System;
using System.Collections.Generic;
//ejemplosasdasdsadsacls
namespace GestionReservasWebII.Models;

public partial class HistorialNormativa
{
    public int NormativaId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime? FechaAceptacion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
