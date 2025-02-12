using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GestionReservasWebII.Models;

namespace GestionReservasWebII.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string role)
        {
            switch (role)
            {
                case "student":
                    return RedirectToAction("DashboardUsuario", "Usuario");
                case "teacher":
                    return RedirectToAction("DocenteDashboard", "Usuario");
                case "admin":
                    return RedirectToAction("DashboardAdministrador", "Usuario");
                default:
                    return View();
            }
        }
    }
}
