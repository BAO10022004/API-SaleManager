using Microsoft.EntityFrameworkCore;
using SaleManagerWebAPI.Data;
using SaleManagerWebAPI.Result;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext BEFORE building the app
builder.Services.AddDbContext<SaleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SaleManagerWebAPI.Services.AuthServices>();
builder.Services.AddScoped<SaleManagerWebAPI.Services.CodeServices>();
builder.Services.AddScoped<SaleManagerWebAPI.Services.EmailServices>();
builder.Services.AddScoped<BaseReponseService>();

var app = builder.Build(); // Build happens here
var options = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// In Program.cs

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Database initialization
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SaleContext>();
    if (app.Environment.IsDevelopment())
    {
        context.Database.EnsureCreated();
    }
}

app.Run();