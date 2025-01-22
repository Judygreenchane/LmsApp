using Domain.Models.Entities;
using LMS.API.Extensions;
using LMS.Infrastructure.Data;
using LMS.Presemtation;
using LMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Microsoft.Extensions.Logging;

namespace LMS.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DbContext
        builder.Services.AddDbContext<LmsContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("LmsContext"),
                b => b.MigrationsAssembly("LMS.Infrastructure")
            ));

        // Register services
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();

        // Add Identity
        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
             .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LmsContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
       

        // Configure Controllers
        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddNewtonsoftJson()
        .AddApplicationPart(typeof(AssemblyReference).Assembly);

        // Configure Additional Services
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureCors();
        builder.Services.ConfigureOpenApi();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        // Configure Repositories and Services
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureServiceLayerServices();

        builder.Services.Configure<PasswordHasherOptions>(options => options.IterationCount = 10000);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            try
            {
                await app.SeedDataAsync();
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }



        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}