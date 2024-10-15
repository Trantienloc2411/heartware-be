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

        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


        PayOS payOS = new PayOS(configuration["PaymentEnvironment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                configuration["PaymentEnvironment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                configuration["PaymentEnvironment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
            builder.Services.AddSingleton(payOS);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddDbContext<MyDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

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

        app.UseAuthorization();
        app.UseCors(CORS_CONFIG);
        app.MapControllers();

        app.Run();
    }
}
