using BusinessObjects.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Implement;
using Service.Services;

namespace HeartwareManagementAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddDbContext<MyDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        var CORS_CONFIG = "_CORS_CONFIG";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CORS_CONFIG,
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors(CORS_CONFIG);
        app.MapControllers();

        app.Run();
    }
}