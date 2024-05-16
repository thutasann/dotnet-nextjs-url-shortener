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

app.MapFallback(async (AppDbContext _db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await _db.Urls.FirstOrDefaultAsync(u => u.ShortUrl.Trim() == path.Trim());

    if (urlMatch == null)
    {
        return Results.BadRequest("Invalid short url");
    }

    return Results.Redirect(urlMatch.Url);
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("myAppCors");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();