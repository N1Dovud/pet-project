using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Helpers;
using WebApp.Services.Database;
using WebApp.Services.DatabaseService;
using WebApp.Services.AuthenticationService;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Users;
using WebApp.Services.ListTaskService;
using WebApp.Services.ToDoListService;
using WebApp.Services.TagService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IToDoListWebApiService, ToDoListWebApiService>();
builder.Services.AddScoped<IListTaskWebApiService, ListTaskWebApiService>();
builder.Services.AddScoped<TagWebApiService, TagWebApiService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("jwt"))
                {
                    context.Token = context.Request.Cookies["jwt"];
                }

                return Task.CompletedTask;
            },
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtDelegatingHandler>();
builder.Services.AddHttpClient("ApiWithJwt")
    .AddHttpMessageHandler<JwtDelegatingHandler>();
builder.Services.AddDbContext<UserDbContext>(options =>
{
    _ = options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 7;
}).AddEntityFrameworkStores<UserDbContext>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IJWTService, JWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
app.MapControllers();

await app.RunAsync();
