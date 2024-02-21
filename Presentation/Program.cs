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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

///////////////////////////////////////

builder.Services.AddTransient<DbContext, Context>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();

builder.Services.AddSingleton<IMapperService, AutoMapperManager>();
builder.Services.AddSingleton<IUserService, UserManager>();

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
    fv.RegisterValidatorsFromAssemblyContaining<UserLoginDtoValidation>();

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
