using CleanArchitecture.Identity.Models; 
using Microsoft.AspNetCore.Identity;
namespace CleanArchitecture.Identity
{
    public class IdentityDbInitializer
    {
        public static async Task SeedAsync( UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) { 
            // Roles 
            if (!await roleManager.RoleExistsAsync(Roles.Administrator)) { 
                await roleManager.CreateAsync( new IdentityRole(Roles.Administrator));
            } 
            if (!await roleManager.RoleExistsAsync(Roles.Employee)) { 
                await roleManager.CreateAsync( new IdentityRole(Roles.Employee)); 
            } 
            // Admin user 
            var adminUser = await userManager.FindByEmailAsync("admin@localhost.com"); 
            if (adminUser == null) 
            { 
                adminUser = new ApplicationUser { Email = "admin@localhost.com", UserName = "admin@localhost.com", FirstName = "System", LastName = "Admin", EmailConfirmed = true }; 
                await userManager.CreateAsync(adminUser, "P@ssword1"); 
                await userManager.AddToRoleAsync(adminUser, Roles.Employee);
                await userManager.AddToRoleAsync(adminUser, Roles.Administrator);
            }
        }   
    }
}