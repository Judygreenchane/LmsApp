using Bogus;
using Domain.Models.Entities;
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
        private static LmsContext context = null!;
        private const string adminRole = "Admin";
        private const string teacherRole = "Teacher";
        private const string studentRole = "Student";
        private const string placeholderPassword = "BytMig123!";
        private const string adminEmail = "admin@lexicon.se";

        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    context = serviceProvider.GetRequiredService<LmsContext>();

                    // Ensure database is created
                    await context.Database.EnsureCreatedAsync();

                    // Apply any pending migrations
                    await context.Database.MigrateAsync();

                    userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>()
                        ?? throw new ArgumentNullException(nameof(userManager));

                    roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>()
                        ?? throw new ArgumentNullException(nameof(roleManager));

                    // Check if data already exists
                    if (!await context.Roles.AnyAsync(r => r.Name == adminRole))
                    {
                        await CreateRolesAsync([adminRole]);
                    }
                    if (!await context.Roles.AnyAsync(r => r.Name == teacherRole))
                    {
                        await CreateRolesAsync([teacherRole]);
                    }
                    if (!await context.Roles.AnyAsync(r => r.Name == studentRole))
                    {
                        await CreateRolesAsync([studentRole]);
                    }

                    if (!await context.Users.AnyAsync(u => userManager.IsInRoleAsync(u, adminRole).Result))
                    {
                        // Create admin user
                        await CreateAdminUserAsync();
                    }

                    if (!await context.Users.AnyAsync(u => userManager.IsInRoleAsync(u, teacherRole).Result))
                    {
                        // Generate teachers
                        await GenerateUsersAsync(2, teacherRole);
                    }

                    if (!await context.Users.AnyAsync(u => userManager.IsInRoleAsync(u, studentRole).Result))
                    {
                        // Generate teachers
                        await GenerateUsersAsync(5, studentRole);
                    }


                    if (!context.ActivityTypes.Any())
                    {
                        // Generate ActivityTypes
                        await GenerateActivitiesTypesAsync(5);
                    }

                    if (!context.Courses.Any())
                    {
                        //Create a Course
                        await CreateCourseAsync();
                    }

                    // Save changes
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    var logger = serviceProvider.GetRequiredService<ILogger<LmsContext>>();
                    logger.LogError(ex, $"An error occurred while seeding the database: {ex.Message}");
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
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admina",
                    LastName = "Adminsson"
                };

                var result = await userManager.CreateAsync(admin, placeholderPassword);
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

        private static async Task GenerateUsersAsync(int nrOfUsers, string role = studentRole)
        {
            var faker = new Faker<ApplicationUser>("sv")
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, (f, u) => u.Email)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.EmailConfirmed, f => true);

            var users = faker.Generate(nrOfUsers);

            foreach (var user in users)
            {
                if (await userManager.FindByEmailAsync(user.Email!) == null)
                {
                    var result = await userManager.CreateAsync(user, placeholderPassword);
                    if (!result.Succeeded)
                        throw new Exception($"Failed to create user {user.Email}: {string.Join("\n", result.Errors)}");
                    else
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        private static async Task CreateCourseAsync()
        {
            var faker = new Faker();
            DateTime startDate = faker.Date.Soon(2);
            DateTime endDate = startDate.AddDays(30);
            var course = new Course()
            {
                Name = ".NET",
                Description = "Programming with .NET and C#",
                StartDate = startDate,
                EndDate = endDate,
                Modules = await GenerateModulesAsync(3, startDate, endDate),
                Documents = await GenerateDocumentsAsync(3)
            };
            await context.Courses.AddAsync(course);

        }
        private static async Task<ICollection<Module>> GenerateModulesAsync(int nrOfModules, DateTime courseStart, DateTime courseEnd)
        {
            var faker = new Faker<Module>("sv")
                .RuleFor(m => m.Description, f => f.Lorem.Paragraph(1));

            var modules = faker.Generate(nrOfModules);
            string[] moduleNames = ["C# Basics", "Visual Studio", ".NET basics", "Debugging", "API"];

            int avarageSpan = ((int)((courseStart - courseEnd)/nrOfModules).TotalDays);

            for (int i = 0; i < nrOfModules; i++)
            {
                var module = modules[i];

                module.Name = moduleNames[i];

                if (i == 0) module.StartDate = courseStart;
                else module.StartDate = modules[i - 1].EndDate.AddDays(1);

                if (i == nrOfModules - 1) module.EndDate = courseEnd;
                else module.EndDate = module.StartDate.AddDays(avarageSpan);
                module.Activities = await GenerateActivitiesAsync(3, module.StartDate, module.EndDate);
                module.Documents = await GenerateDocumentsAsync(2);
            }
            return modules;
        }

        private static async Task<ICollection<Activity>> GenerateActivitiesAsync(int nrOfActivities, DateTime moduleStart, DateTime moduleEnd)
        {
            var faker = new Faker<Activity>("sv")
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph(1))
                .RuleFor(a => a.Name, f => f.Company.CatchPhrase());

            var activities = faker.Generate(nrOfActivities);

            int avarageSpan = ((int)((moduleStart - moduleEnd) / nrOfActivities).TotalDays);

            for (int i = 0; i < nrOfActivities; i++)
            {
                var activity = activities[i];

                if (i == 0) activity.StartTime = moduleStart;
                else activity.StartTime = activities[i - 1].EndTime.AddDays(1);

                if (i == nrOfActivities - 1) activity.EndTime = moduleEnd;
                else activity.EndTime = activity.StartTime.AddDays(avarageSpan);
                activity.ActivityType = await context.ActivityTypes.FirstOrDefaultAsync(); //Todo
                activity.Documents = await GenerateDocumentsAsync(2);
            }
            return activities;
        }

        private static async Task GenerateActivitiesTypesAsync(int nrOfActivities)
        {
            var faker = new Faker<ActivityType>("sv")
                .RuleFor(at => at.Name, f => f.Name.JobTitle());
            await context.ActivityTypes.AddRangeAsync(faker.Generate(nrOfActivities));
        }

        private static async Task<ICollection<Document>> GenerateDocumentsAsync(int nrOfDocuments)
        {
            var faker = new Faker<Document>("sv")
                .RuleFor(d => d.Name, f => f.Lorem.Word())
                .RuleFor(d => d.Description, f => f.Lorem.Text())
                .RuleFor(d => d.FilePath, f => f.System.FilePath());

            var documents = faker.Generate(nrOfDocuments);
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin != null)
            {
                foreach (Document document in documents)
                {
                    document.User = admin;
                }
            }
            else throw new Exception("No admin found");

            return documents;
        }

    }
}