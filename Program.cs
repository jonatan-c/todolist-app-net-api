using todolistapi.Interfaces;
using todolistapi.Models;
using todolistapi.Models.DataModels;
using todolistapi.Services;
using todolistapi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB CONTEXT
builder.Services.AddSqlServer<TodolistContext>(builder.Configuration.GetConnectionString("ToDoListConnection"));

//TODO Inject Services Layer
builder.Services.AddScoped<IRepository<Tasklist>, TaskListRepository>();
builder.Services.AddScoped<TaskListService , TaskListService>();

var app = builder.Build();

app.UseCors(builder => 
   builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
