using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Options;
using ZenBlog.Domain.Entities;
using ZenBlog.Persistence.Concrete;
using ZenBlog.Persistence.Context;
using ZenBlog.Persistence.Interceptors;


namespace ZenBlog.Persistence.Extensions
{
    public static class ServiceRegistrations
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), sqlOptions => { });
                options.AddInterceptors(new AuditDbContextInterceptor());
                options.UseLazyLoadingProxies();
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IUnitOfWork, UnıtOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IJwtService,JwtService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    var jwtTokenOptions = configuration.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();

                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtTokenOptions.Issuer,
                        ValidAudience = jwtTokenOptions.Audience,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        

    }
}
