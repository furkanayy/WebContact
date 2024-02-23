using DataAccess.Concrete.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Business.Abstract.UnitOfWork;
using Business.Concrete.UnitOfWork;
using Business.Abstract.Service;
using Business.Concrete.Manager;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.EfDals;
using FluentValidation.AspNetCore;
using Business.Concrete.Utilities.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

///////////////////////////////////////

builder.Services.AddTransient<DbContext, Context>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();

builder.Services.AddSingleton<IMapperService, AutoMapperManager>();
builder.Services.AddSingleton<IUserService, UserManager>();

#pragma warning disable CS0618 // AddFluentValidation is obsolete
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv =>
    {
        //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
        fv.RegisterValidatorsFromAssemblyContaining<UserLoginDtoValidation>();
    });
#pragma warning restore CS0618 // AddFluentValidation is obsolete

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "CookieAuthentication";
    options.DefaultSignInScheme = "CookieAuthentication";
    options.DefaultSignOutScheme = "CookieAuthentication";
    options.RequireAuthenticatedSignIn = false;
})
.AddCookie("CookieAuthentication", config =>
{
    config.Cookie.Name = "UserLoginCookie";
    config.LoginPath = "/Login"; // Giriþ yapýlacak sayfa
    config.LogoutPath = "/Logout"; // Çýkýþ yapýlacak sayfa
    config.AccessDeniedPath = "/Error/AccessDenied"; // Yetki olmayan sayfa
});
//////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{area=Admin}/{controller=Admin}/{action=Login}");

    endpoints.MapGet("/",async context =>
    {
        context.Response.Redirect("/Login");
    });
});

app.Run();
