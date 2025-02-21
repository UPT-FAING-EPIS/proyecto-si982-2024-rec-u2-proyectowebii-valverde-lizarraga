using System;

namespace GestionReservasWebII.Models
{
    public class RecursoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string RegistradoPor { get; set; } // Added property
    }
}