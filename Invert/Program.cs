using Invert.DAL;
using Invert.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{

    opt.Password.RequireNonAlphanumeric = false;
}

).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();





builder.Services.AddDbContext<AppDbContext>(opt =>

{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
}

);









var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllerRoute(

    "admin",
    "{area:exists}/{controller=home}/{action=index}/{id?}"

);
app.MapControllerRoute(

    "default",
    "{controller=home}/{action=index}/{id?}"

);

app.Run();
