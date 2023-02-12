using FadiBackEndApI.Models;
using FadiBackEndApI.Services.Contract;
using FadiBackEndApI.Services.Implementation;
using FadiBackEndApI.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FadiDbContext>(options => 
    options.UseOracle(builder.Configuration.GetConnectionString("FadiConnectionString"))
);

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeSerivce, EmployeeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FadiAPIPolicyToAngularApplication", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FadiAPIPolicyToAngularApplication");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
