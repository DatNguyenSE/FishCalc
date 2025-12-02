using FishCalc.Web.Data;
using FishCalc.Web.Interfaces;
using FishCalc.Web.Interfaces.IServices;
using FishCalc.Web.Repositories;
using FishCalc.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IFishTypeRepository, FishTypeRepository>();
builder.Services.AddScoped<IFishTypeService, FishTypeService>();
builder.Services.AddScoped<IProcessingUnitRepository, ProcessingUnitRepository>();
builder.Services.AddScoped<IProcessingUnitService, ProcessingUnitService>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
   opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // connect with sql server


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

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
