using System;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Author: Valerie German
/// Date: 22 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: Startup file, configures user services.
/// </summary>
[assembly: HostingStartup(typeof(CS4540PS2.Areas.Identity.IdentityHostingStartup))]
namespace CS4540PS2.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<UserContext>();
            });
        }
    }
}