using GestionReservasWebII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GestionReservasWebII.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult DashboardUsuario()
        {
            return View();
        }

        public IActionResult DocenteDashboard()
        {
            return View();
        }

        public IActionResult DashboardAdministrador()
        {
            return View();
        }
    }
}