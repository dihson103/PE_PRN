using Microsoft.EntityFrameworkCore;
using Q2.Hubs;
using Q2.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PRN_Spr23_B1Context>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();


builder.Services.AddSignalR();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapHub<EmployeeHub>("/employeeHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
