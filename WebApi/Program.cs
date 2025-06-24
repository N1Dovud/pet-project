using Microsoft.EntityFrameworkCore;
using WebApi.Services.Database;
using WebApi.Services.DatabaseService;

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
builder.Services.AddScoped<IToDoListDatabaseService, ToDoListDatabaseService>();

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
