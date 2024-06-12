using BusinessLogicLayer.ManagerClasses;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using System.Reflection;
using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLogicLayer.RecommendationAlgorithm;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


// Load the DAL assembly
var dalAssemblyPath = "../DataAccessLayer/bin/debug/net8.0/DataAccessLayer.dll"; // Ensure this path is correct
var dalAssembly = Assembly.LoadFrom(dalAssemblyPath);
var userDataAccessType = dalAssembly.GetType("DataAccessLayer.UserDataAccess", throwOnError: true);

var bookDataAccessType = dalAssembly.GetType("DataAccessLayer.BookDataAccess", throwOnError: true);
var bookshelfDataAccessType = dalAssembly.GetType("DataAccessLayer.BookshelfDataAccess", throwOnError: true);
var bookFormatDataAccessType = dalAssembly.GetType("DataAccessLayer.BookFormatDataAccess", throwOnError: true);
var genreDataAccessType = dalAssembly.GetType("DataAccessLayer.GenreDataAccess", throwOnError: true);
var reviewDataAccessType = dalAssembly.GetType("DataAccessLayer.ReviewDataAccess", throwOnError: true);
var roleDataAccessType = dalAssembly.GetType("DataAccessLayer.RoleDataAccess", throwOnError: true);

// Register DAL services using reflection


builder.Services.AddScoped(typeof(IUserDataAccess), userDataAccessType);
builder.Services.AddScoped(typeof(IBookDataAccess), bookDataAccessType);
builder.Services.AddScoped(typeof(IBookshelfDataAccess), bookshelfDataAccessType);
builder.Services.AddScoped(typeof(IBookFormatDataAccess), bookFormatDataAccessType);
builder.Services.AddScoped(typeof(IGenreDataAccess), genreDataAccessType);
builder.Services.AddScoped(typeof(IReviewDataAccess), reviewDataAccessType);
builder.Services.AddScoped(typeof(IRoleDataAccess), roleDataAccessType);

// Register BLL services
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IReviewManager, ReviewManager>();
builder.Services.AddScoped<IBookManager, BookManager>();
builder.Services.AddScoped<IBookshelfManager, BookshelfManager>();
builder.Services.AddScoped<IBookFormatManager, BookFormatManager>();
builder.Services.AddScoped<IGenreManager, GenreManager>();
builder.Services.AddScoped<IRoleManager, RoleManager>();
builder.Services.AddScoped<UserDTO>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<PasswordAuthenticator>();

// Register Recommendation algorithm
builder.Services.AddScoped<RecommendationEngine>();



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
