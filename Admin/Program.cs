using Application.ContextInterfaces;
using Infrastructure.AddIdentityDatabaseToIservice;
using Infrastructure.ImageServices;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();






builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));
builder.Services.AddMyIdentityService(builder.Configuration);


builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<IImageServices, ImageService>();


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
