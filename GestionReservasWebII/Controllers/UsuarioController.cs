using GestionReservasWebII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GestionReservasWebII.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ShortenContext _context;

        public UsuarioController(ShortenContext context)
        {
            _context = context;
        }

        // 👉 GET: Formulario de Registro de Estudiantes
        [HttpGet]
        public IActionResult RegistrarEstudiante()
        {
            return View("RegistrarEstudiante");
        }

        // 👉 POST: Procesar Registro de Estudiantes
        [HttpPost]
        public IActionResult RegistrarEstudiante(string nombre, string apellido, string codigoUniversitario, string telefono, string password)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(codigoUniversitario) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View("RegistrarEstudiante");
            }

            if (_context.Estudiantes.Any(e => e.CodigoUniversitario == codigoUniversitario))
            {
                ViewBag.ErrorMessage = "El código universitario ya está registrado.";
                return View("RegistrarEstudiante");
            }

            if (!Regex.IsMatch(password, "^[a-zA-Z0-9]+$"))
            {
                ViewBag.ErrorMessage = "La contraseña solo debe contener letras y números.";
                return View("RegistrarEstudiante");
            }

            string hashedPassword = HashPassword(password);

            var nuevoEstudiante = new Estudiante
            {
                Nombre = nombre,
                Apellido = apellido,
                CodigoUniversitario = codigoUniversitario,
                Telefono = telefono,
                PasswordHash = hashedPassword,
                Estado = true,
                IdRol = _context.Roles.FirstOrDefault(r => r.NombreRol == "Estudiante")?.IdRol ?? 1
            };

            _context.Estudiantes.Add(nuevoEstudiante);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // 👉 GET: Formulario de Registro de Docentes
        [HttpGet]
        public IActionResult RegistrarDocente()
        {
            return View("RegistrarDocente");
        }

        // 👉 POST: Procesar Registro de Docentes
        [HttpPost]
        public IActionResult RegistrarDocente(string nombre, string apellido, string correo, string telefono, string password)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View("RegistrarDocente");
            }

            if (!correo.EndsWith("@virtual.upt.pe"))
            {
                ViewBag.ErrorMessage = "El correo del docente debe ser del dominio virtual.upt.pe.";
                return View("RegistrarDocente");
            }

            if (_context.Docentes.Any(d => d.Correo == correo))
            {
                ViewBag.ErrorMessage = "El correo ya está registrado.";
                return View("RegistrarDocente");
            }

            if (!Regex.IsMatch(password, "^[a-zA-Z0-9]+$"))
            {
                ViewBag.ErrorMessage = "La contraseña solo debe contener letras y números.";
                return View("RegistrarDocente");
            }

            string hashedPassword = HashPassword(password);

            var nuevoDocente = new Docente
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Telefono = telefono,
                PasswordHash = hashedPassword,
                Estado = true,
                IdRol = _context.Roles.FirstOrDefault(r => r.NombreRol == "Docente")?.IdRol ?? 2
            };

            _context.Docentes.Add(nuevoDocente);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // 👉 GET: Formulario de Registro de Administradores
        [HttpGet]
        public IActionResult RegistrarAdministrador()
        {
            return View("RegistrarAdministrador");
        }

        // 👉 POST: Procesar Registro de Administradores
        [HttpPost]
        public IActionResult RegistrarAdministrador(string dni, string nombre, string apellido, string correo, string telefono, string password)
        {
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View("RegistrarAdministrador");
            }

            if (!Regex.IsMatch(dni, "^[0-9]{8}$"))
            {
                ViewBag.ErrorMessage = "El DNI debe contener exactamente 8 dígitos numéricos.";
                return View("RegistrarAdministrador");
            }

            if (_context.Administradores.Any(a => a.Dni == dni))
            {
                ViewBag.ErrorMessage = "El DNI ya está registrado.";
                return View("RegistrarAdministrador");
            }

            if (!Regex.IsMatch(password, "^[a-zA-Z0-9]+$"))
            {
                ViewBag.ErrorMessage = "La contraseña solo debe contener letras y números.";
                return View("RegistrarAdministrador");
            }

            string hashedPassword = HashPassword(password);

            var nuevoAdministrador = new Administradore
            {
                Dni = dni,
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Telefono = telefono,
                PasswordHash = hashedPassword,
                Estado = true,
                IdRol = _context.Roles.FirstOrDefault(r => r.NombreRol == "Administrador")?.IdRol ?? 3
            };

            _context.Administradores.Add(nuevoAdministrador);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // 👉 GET: Dashboard de Estudiante
        [HttpGet]
        public IActionResult DashboardUsuario()
        {
            var usuario = _context.Estudiantes.FirstOrDefault(u => u.CodigoUniversitario == HttpContext.Session.GetString("Username"));

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Nombre = usuario.Nombre;
            ViewBag.ActividadesRecientes = _context.HistorialReservas
                .Where(h => h.IdEstudiante == usuario.IdEstudiante)
                .OrderByDescending(h => h.FechaModificacion)
                .Take(4)
                .ToList();

            return View("DashboardUsuario");
        }

        // 👉 GET: Dashboard de Docente
        [HttpGet]
        public IActionResult DashboardDocente()
        {
            var docente = _context.Docentes.FirstOrDefault(d => d.Correo == HttpContext.Session.GetString("Username"));

            if (docente == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Nombre = docente.Nombre;
            ViewBag.ActividadesRecientes = _context.HistorialReservas
                .Where(h => h.IdEstudiante == docente.IdDocente)
                .OrderByDescending(h => h.FechaModificacion)
                .Take(4)
                .ToList();

            return View("DashboardDocente");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
