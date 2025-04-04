using BusinessObjects.Context;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Repository.Implement;
using Service.IService;
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

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


        PayOS payOS = new PayOS(configuration["PaymentEnvironment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                configuration["PaymentEnvironment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                configuration["PaymentEnvironment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
            builder.Services.AddSingleton(payOS);
        builder.Services.AddDbContext<MyDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("Deploy")));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IPayment, Payment>();

        var CORS_CONFIG = "_CORS_CONFIG";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CORS_CONFIG,
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
        

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseCors(CORS_CONFIG);
        app.MapControllers();

        app.Run();
    }
}

