using Microsoft.EntityFrameworkCore;
using server.Data;

string[] allowedOrigin = ["http://localhost:3000"];
var builder = WebApplication.CreateBuilder(args);

// http context
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

// ------ Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("myAppCors");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();