using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register BLL services
builder.Services.AddScoped<UserManager>();

// Load the DAL assembly
var dalAssemblyPath = "../DataAccessLayer/bin/debug/net8.0/DataAccessLayer.dll"; // Ensure this path is correct
var dalAssembly = Assembly.LoadFrom(dalAssemblyPath);
var userDataAccessType = dalAssembly.GetType("DataAccessLayer.UserDataAccess");

// Register DAL services using reflection
builder.Services.AddScoped(typeof(IUserDataAccess), userDataAccessType);

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

app.Run();
