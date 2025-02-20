using GestionReservasWebII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestionReservasWebII.Controllers
{
    public class RecursosController : Controller
    {
        private readonly ShortenContext _context;

        public RecursosController(ShortenContext context)
        {
            _context = context;
        }

        // 👉 LISTAR RECURSOS
        public async Task<IActionResult> GestionarRecursosAdministrador()
        {
            var recursos = await _context.Aulas
                .Select(a => new {
                    Id = a.IdAula,
                    Tipo = "Aula",
                    Nombre = a.Nombre,
                    Cantidad = a.CantidadCarpetas,
                    Fecha = a.FechaRegistro,
                    Estado = a.IdEstadoNavigation.NombreEstado,
                    RegistradoPor = a.RegistradoPorNavigation.Nombre
                })
                .Union(_context.Laboratorios
                    .Select(l => new {
                        Id = l.IdLaboratorio,
                        Tipo = "Laboratorio",
                        Nombre = l.Nombre,
                        Cantidad = l.CantidadComputadoras,
                        Fecha = l.FechaRegistro,
                        Estado = l.IdEstadoNavigation.NombreEstado,
                        RegistradoPor = l.RegistradoPorNavigation.Nombre
                    }))
                .ToListAsync();
            
            ViewBag.Recursos = recursos;
            return View();
        }

        // 👉 FORMULARIO DE REGISTRO DE RECURSOS
        [HttpGet]
        public IActionResult AgregarRecursoAdministrador()
        {
            return View();
        }

        // 👉 PROCESAR REGISTRO DE RECURSOS
        [HttpPost]
        public async Task<IActionResult> AgregarRecursoAdministrador(string nombre, string tipo, int cantidad, string estado, DateTime fechaRegistro, string[] horarios)
        {
            if (string.IsNullOrEmpty(nombre) || cantidad <= 0 || string.IsNullOrEmpty(estado))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View();
            }

            if (tipo == "Aula")
            {
                var nuevaAula = new Aula
                {
                    Nombre = nombre,
                    CantidadCarpetas = cantidad,
                    IdEstado = estado == "Activo" ? 1 : 2,
                    FechaRegistro = fechaRegistro,
                    RegistradoPor = int.Parse(HttpContext.Session.GetString("UserId"))
                };
                _context.Aulas.Add(nuevaAula);
                await _context.SaveChangesAsync();
                
                await GuardarHorarios(nuevaAula.IdAula, "Aula", horarios);
            }
            else if (tipo == "Laboratorio")
            {
                var nuevoLaboratorio = new Laboratorio
                {
                    Nombre = nombre,
                    CantidadComputadoras = cantidad,
                    IdEstado = estado == "Activo" ? 1 : 2,
                    FechaRegistro = fechaRegistro,
                    RegistradoPor = int.Parse(HttpContext.Session.GetString("UserId"))
                };
                _context.Laboratorios.Add(nuevoLaboratorio);
                await _context.SaveChangesAsync();
                
                await GuardarHorarios(nuevoLaboratorio.IdLaboratorio, "Laboratorio", horarios);
            }

            return RedirectToAction("GestionarRecursosAdministrador");
        }

        // 👉 FORMULARIO DE EDICIÓN DE RECURSOS
        [HttpGet]
        public async Task<IActionResult> EditarRecursoAdministrador(int id, string tipo)
        {
            object recurso = null;
            if (tipo == "Aula")
            {
                recurso = await _context.Aulas.Include(a => a.DisponibilidadHorarios).FirstOrDefaultAsync(a => a.IdAula == id);
            }
            else if (tipo == "Laboratorio")
            {
                recurso = await _context.Laboratorios.Include(l => l.DisponibilidadHorarios).FirstOrDefaultAsync(l => l.IdLaboratorio == id);
            }

            if (recurso == null)
            {
                return NotFound();
            }

            return View(recurso);
        }

        // 👉 PROCESAR EDICIÓN DE RECURSOS
        [HttpPost]
        public async Task<IActionResult> EditarRecursoAdministrador(int id, string tipo, string nombre, int cantidad, string estado, DateTime fechaRegistro, string[] horarios)
        {
            if (tipo == "Aula")
            {
                var aula = await _context.Aulas.FindAsync(id);
                if (aula != null)
                {
                    aula.Nombre = nombre;
                    aula.CantidadCarpetas = cantidad;
                    aula.IdEstado = estado == "Activo" ? 1 : 2;
                    aula.FechaRegistro = fechaRegistro;
                    await _context.SaveChangesAsync();
                    
                    await GuardarHorarios(id, "Aula", horarios);
                }
            }
            else if (tipo == "Laboratorio")
            {
                var laboratorio = await _context.Laboratorios.FindAsync(id);
                if (laboratorio != null)
                {
                    laboratorio.Nombre = nombre;
                    laboratorio.CantidadComputadoras = cantidad;
                    laboratorio.IdEstado = estado == "Activo" ? 1 : 2;
                    laboratorio.FechaRegistro = fechaRegistro;
                    await _context.SaveChangesAsync();
                    
                    await GuardarHorarios(id, "Laboratorio", horarios);
                }
            }

            return RedirectToAction("GestionarRecursosAdministrador");
        }

        // 👉 ELIMINAR RECURSO
        [HttpPost]
        public async Task<IActionResult> EliminarRecurso(int id, string tipo)
        {
            if (tipo == "Aula")
            {
                var aula = await _context.Aulas.FindAsync(id);
                if (aula != null)
                {
                    _context.Aulas.Remove(aula);
                    await _context.SaveChangesAsync();
                }
            }
            else if (tipo == "Laboratorio")
            {
                var laboratorio = await _context.Laboratorios.FindAsync(id);
                if (laboratorio != null)
                {
                    _context.Laboratorios.Remove(laboratorio);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("GestionarRecursosAdministrador");
        }

    private async Task GuardarHorarios(int recursoId, string tipo, string[] horarios)
{
    foreach (var horario in horarios)
    {
        var partesHorario = horario.Split('-'); // Divide la cadena de horario en inicio y fin
        var horaInicio = TimeSpan.Parse(partesHorario[0]);
        var horaFin = TimeSpan.Parse(partesHorario[1]);

        var nuevoHorario = new DisponibilidadHorario
        {
            IdAula = tipo == "Aula" ? recursoId : (int?)null,
            IdLaboratorio = tipo == "Laboratorio" ? recursoId : (int?)null,
            HoraInicio = TimeOnly.FromTimeSpan(horaInicio), // Conversión correcta
            HoraFin = TimeOnly.FromTimeSpan(horaFin)        // Conversión correcta
        };

        _context.DisponibilidadHorarios.Add(nuevoHorario);
    }
    await _context.SaveChangesAsync();
}

    }
}
