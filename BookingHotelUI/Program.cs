using Application.ContextInterfaces;
using Application.Services.CommentServices.CommentFacadeService;
using Application.Services.PaymentServices.PaymentFacadeService;
using Application.Services.UserHotelServices.UserFacadeHotelService;
using Infrastructure.AddIdentityDatabaseToIservice;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));
builder.Services.AddMyIdentityService(builder.Configuration);

builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<IUserHotelService, UserHotelService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<ICommentService, CommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
