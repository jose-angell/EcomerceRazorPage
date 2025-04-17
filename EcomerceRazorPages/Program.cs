using ECommerce.DataAccess;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"))
);

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//soporte para cookies de autenticacion y autorizacion 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // ruta predenterminada para iniciar sesion
    options.LogoutPath = "/Identity/Account/Logout"; // ruta predenterminada para cerrar sesion
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // ruta predenterminada para acceso denegado
    options.Cookie.HttpOnly = true; // mejora la seguridad al prevenir acceso del lado cliente a las cookies
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Duracion de la cookie antes de expirar
    options.SlidingExpiration= true; // Renueva la cookie si el usuario permacene activo
});

// Add services to the container.
builder.Services.AddRazorPages();

//Agregar soporte para EmailSender
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//Agregar Repositorios al contenedor de Inyeccion de dependencias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Redirigir manuamente la pagina deseada
app.MapGet("/",context => {
    context.Response.Redirect("/Cliente/Inicio/Index");
    return Task.CompletedTask;
});

app.Run();
