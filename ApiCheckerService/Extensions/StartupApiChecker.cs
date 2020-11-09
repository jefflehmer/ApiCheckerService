using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCheckerService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCheckerService.Extensions
{
    public static class StartupApiChecker
    {
        public static IServiceCollection SetupReports(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IReportService), typeof(ReportService));

            return services;
        }
    }
}
