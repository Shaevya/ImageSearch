using ImageSearchServer.Core;
using ImageSearchServer.BusinessLogicLayer;
using ImageSearchServer.Contracts;
using ImageSearchServer.DataAccesslayer;
using Serilog;
using SharedUtility;
using ImageSearchServer.DataAccesslayer.Utility;

namespace ImageSearchServer
{
    public class Program
    {
        private static string DEFAULT_LOGS_PATH = $"{Path.GetTempPath()}//IMLogs//Server//IMlogs.txt";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((host, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.File(host.Configuration[ServerConstants.AppSettingsKey.LogsFolderPath] ?? DEFAULT_LOGS_PATH, rollingInterval: RollingInterval.Day);
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            builder.Services.AddScoped<Contracts.IHttpRequestUtility, HttpRequestUtility>();
            builder.Services.AddScoped<IBusinessLogic, BusinessLogic>();
            builder.Services.AddScoped<IApplicationDAL, ApplicationDAL>();
            builder.Services.AddScoped<ICustomHttpClientFactory, CustomHttpClientFactory>();

            var app = builder.Build();

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
        }
    }
}