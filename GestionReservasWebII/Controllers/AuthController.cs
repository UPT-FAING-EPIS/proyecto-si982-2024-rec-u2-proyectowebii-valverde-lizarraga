using Microsoft.AspNetCore.Mvc;
using GestionReservasWebII.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GestionReservasWebII.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShortenContext _context;

        public AuthController(ShortenContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string role, string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Todos los campos son obligatorios.";
                return View("~/Views/Home/Index.cshtml");
            }

            string hashedPassword = HashPassword(password);

            object user = null;
            string redirectAction = "Index";
            string redirectController = "Home";

           switch (role)
{
    case "student":
        user = _context.Estudiantes.FirstOrDefault(u => u.CodigoUniversitario == username && u.PasswordHash == hashedPassword && (u.Estado ?? false));
        redirectAction = "DashboardUsuario";
        redirectController = "Usuario";
        break;

    case "teacher":
        if (!username.EndsWith("@virtual.upt.pe"))
        {
            ViewBag.ErrorMessage = "El correo del docente debe ser de dominio virtual.upt.pe";
            return View("~/Views/Home/Index.cshtml");
        }
        user = _context.Docentes.FirstOrDefault(u => u.Correo == username && u.PasswordHash == hashedPassword && (u.Estado ?? false));
        redirectAction = "DocenteDashboard";
        redirectController = "Usuario";
        break;

    case "admin":
        user = _context.Administradores.FirstOrDefault(u => u.Correo == username && u.PasswordHash == hashedPassword && (u.Estado ?? false));
        redirectAction = "DashboardAdministrador";
        redirectController = "Usuario";
        break;
}


            if (user == null)
            {
                ViewBag.ErrorMessage = "Credenciales incorrectas o usuario inactivo.";
                return View("~/Views/Home/Index.cshtml");
            }

            // Guardar información del usuario en la sesión
            HttpContext.Session.SetString("UserRole", role);
            HttpContext.Session.SetString("Username", username);

            return RedirectToAction(redirectAction, redirectController);
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
