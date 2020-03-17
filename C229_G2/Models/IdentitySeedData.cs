using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public static class IdentitySeedData
    {
        private const string admin = "Admin";
        private const string adminPassword = "Secret123$";

        private const string general = "General";
        private const string generalPassword = "Secret123$";


        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var roleManager = app.ApplicationServices
                .GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();

            var roleAdmin = await roleManager.FindByIdAsync(admin);
            if (roleAdmin == null)
            {
                await roleManager.CreateAsync(new IdentityRole(admin));
            }

            var roleGeneral = await roleManager.FindByIdAsync(general);
            if (roleGeneral == null)
            {
                await roleManager.CreateAsync(new IdentityRole(general));
            }

            IdentityUser adminUser = await userManager.FindByIdAsync(IdentitySeedData.admin);
            if (adminUser == null)
            {
                adminUser = new IdentityUser("Admin");
                await userManager.CreateAsync(adminUser, adminPassword);
            }

            IdentityUser generalUser = await userManager.FindByIdAsync(IdentitySeedData.general);
            if (generalUser == null)
            {
                generalUser = new IdentityUser("General");
                await userManager.CreateAsync(generalUser, generalPassword);
            }

            //adminUser => (adminRole)
            await userManager.AddToRoleAsync(adminUser, admin);

            //generalUser => (generalRole)
            await userManager.AddToRoleAsync(generalUser, general);
        }
    }
}
