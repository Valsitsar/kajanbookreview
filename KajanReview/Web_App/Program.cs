using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using System.Reflection;
using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register BLL services
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<UserDTO>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<PasswordAuthenticator>();
// Load the DAL assembly
var dalAssemblyPath = "../DataAccessLayer/bin/debug/net8.0/DataAccessLayer.dll"; // Ensure this path is correct
var dalAssembly = Assembly.LoadFrom(dalAssemblyPath);
var userDataAccessType = dalAssembly.GetType("DataAccessLayer.UserDataAccess", throwOnError: true);

// Register DAL services using reflection
builder.Services.AddScoped(typeof(IUserDataAccess), userDataAccessType);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/SignIn";
        options.AccessDeniedPath = "/SignIn";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.ReturnUrlParameter = "ReturnUrl";
    });


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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
