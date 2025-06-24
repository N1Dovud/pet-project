using Microsoft.EntityFrameworkCore;
using WebApi.Services.Authentication;
using WebApi.Services.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoListDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ToDoListDatabase");
    _ = options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
