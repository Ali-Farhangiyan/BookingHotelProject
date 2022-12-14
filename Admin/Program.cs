using Application.ContextInterfaces;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Application.Services.CommentServices.CommentFacadeService;
using Infrastructure.AddIdentityDatabaseToIservice;
using Infrastructure.ImageServices;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(option =>
{
    //option.Conventions.AddPageRoute("/AdminHotel/ShowHotels", "/");
});






builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));
builder.Services.AddMyIdentityService(builder.Configuration);


builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<IImageServices, ImageService>();
builder.Services.AddTransient<IAdminHotelService, AdminHotelService>();
builder.Services.AddTransient<ICommentService, CommentService>();


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
