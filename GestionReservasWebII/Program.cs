using GestionReservasWebII.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurar la conexión a Azure SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 🔹 Registrar el contexto de base de datos principal
builder.Services.AddDbContext<ShortenContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 🔹 Configurar Identity con la base de datos
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // ⚠️ Cambiar si necesitas confirmación de cuenta
})
.AddEntityFrameworkStores<ShortenContext>(); // 📌 Usar `ShortenContext` en lugar de `ApplicationDbContext`

// 🔹 Habilitar sesiones para mantener la autenticación
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sesión expira en 30 minutos
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 Agregar soporte para MVC con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Configurar el pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 🔹 Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // 📌 Importante para mantener la sesión activa

// 🔹 Configurar rutas de la aplicación
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
