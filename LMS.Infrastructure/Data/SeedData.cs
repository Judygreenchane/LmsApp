using Bogus;
using Domain.Models.Entities;
using LMS.Shared.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LMS.Infrastructure.Data
{
    public static class SeedData
    {
        private static UserManager<ApplicationUser> userManager = null!;
        private static RoleManager<IdentityRole> roleManager = null!;
        private const string adminRole = "Admin";

        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var db = serviceProvider.GetRequiredService<LmsContext>();

                    // Ensure database is created
                    await db.Database.EnsureCreatedAsync();

                    // Apply any pending migrations
                    await db.Database.MigrateAsync();

                    // Check if data already exists
                    if (await db.Users.AnyAsync()) return;

                    userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>()
                        ?? throw new ArgumentNullException(nameof(userManager));

                    roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>()
                        ?? throw new ArgumentNullException(nameof(roleManager));

                    // Create roles
                    await CreateRolesAsync([adminRole]);

                    // Generate users
                    await GenerateUsersAsync(5);

                    // Create admin user
                    await CreateAdminUserAsync();

                    // Save changes
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    var logger = serviceProvider.GetRequiredService<ILogger<LmsContext>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }
        }

        private static async Task CreateRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    throw new Exception($"Failed to create role {roleName}: {string.Join("\n", result.Errors)}");
            }
        }

        private static async Task CreateAdminUserAsync()
        {
            var adminEmail = "admin@lexicon.se";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "BytMig123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRole);
                }
                else
                {
                    throw new Exception($"Failed to create admin user: {string.Join("\n", result.Errors)}");
                }
            }
        }

        private static async Task GenerateUsersAsync(int nrOfUsers)
        {
            var faker = new Faker<ApplicationUser>("sv")
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, (f, u) => u.Email)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.EmailConfirmed, f => true);

            var users = faker.Generate(nrOfUsers);
            var password = "BytMig123!";

            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email!) == null)
                {
                    var result = await userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                        throw new Exception($"Failed to create user {user.Email}: {string.Join("\n", result.Errors)}");
                }
            }
        }
    }
}