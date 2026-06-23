using CleanArchitecture.Identity.Context;
using CleanArchitecture.Identity.Models;
using CleanArchitecture.Identity.Services;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using CleanArchitecture.Identity.Configurations;
namespace CleanArchitecture.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));   
            var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");
            if (useInMemoryDatabase)
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDB"));
                var roles = new RoleConfiguration();
            }
            else
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DatabaseConnectionString")));
            }
            
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            
            /*services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();*/
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var jwtKey = configuration["JwtSettings:Key"]
                    ?? throw new InvalidOperationException(
                        "JWT Key is not configured.");
                        
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Administrator))
                .AddPolicy("EmployeeOnly", policy => policy.RequireRole(Roles.Employee));
            return services;
        }
    }
}