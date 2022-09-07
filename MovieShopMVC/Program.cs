using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShopMVC.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<MovieShopDbContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("MovieShopDbConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.Cookie.Name = "MovieShopAuthCookie";
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
            options.LoginPath = "/account/login";
        }
    );

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

//Middlewares
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //controls the default/main home page
    pattern: "{controller=Home}/{action=Index}/{id?}"); 
    //pattern: "{controller=Movie}/{action=MoviesByGenre}/{id}/{pageSize}/{pageNumber}");

    //debug
    //pattern: "{controller=Movies}/{action=Details}/{id?}");

app.Run();
