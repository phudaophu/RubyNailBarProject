using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Components;
using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;
using RubyNailBarWeb.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


builder.Services.AddDbContextFactory<NailsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NailsDbConnection"));

});

builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<StoresRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IStoresService, StoresService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
