using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

var configuration = builder.Configuration;
string connectionString = "data source=ACER\\CYNOSUREDBS; Initial Catalog = SchoolManagementSystem; Integrated Security = true; TrustServerCertificate=true";
configuration["ConnectionStrings:CustomConnection"] = connectionString;


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Log}/{action=LogIn}/{id?}");

app.Run();
