using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Data;
using CS4540PS2.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CS4540PS2 {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<LearningOutcomeDBContext>();
                    DbInitializer.Initialize(context);
                    var userContext = services.GetRequiredService<UserContext>();
                    DbInitializer.InitializeUser(userContext);
                } catch (Exception e) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
