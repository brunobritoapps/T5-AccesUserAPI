using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class AccessInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AccessContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any access
                if (context.Access != null && context.Access.Any())
                    return;   // DB has already been seeded

                var access = GetAccess().ToArray();
                context.Access.AddRange(access);
                context.SaveChanges();
            }
        }

        public static List<Access> GetAccess()
        {
            List<Access> access = new List<Access>() {
                new Access {
                            Ip ="000.000.00.000",
                            Page = "https://www.google.com",
                            Browser = "Google Chrome Version 75.0.3770.100 (Official Build) (64-bit)",
                            Parameters = "'Ip=000.000.00.000','Page=www.google.com',Browser=Google Chrome",
                            Date = DateTime.Now.ToString(),
                            },
            };
            return access;
        }
    }
}

