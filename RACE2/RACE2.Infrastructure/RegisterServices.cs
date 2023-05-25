using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RACE2.DatabaseProvider.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Infrastructure
{
    public  static class RegisterServices
    {

        public static IConfiguration _configuration;         
       
        public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            var options = new ApplicationInsightsServiceOptions { ConnectionString = configuration["AppInsightsConnectionString"] };
            services.AddApplicationInsightsTelemetry(options: options);
            return services;
        }

        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlconnectionString = configuration["SqlConnectionString"];
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(sqlconnectionString));
            return services;
        }

    }
}
