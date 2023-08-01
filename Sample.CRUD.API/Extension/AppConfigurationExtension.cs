using Microsoft.EntityFrameworkCore;
using Sample.CRUD.Model.Common;
using Sample.CRUD.Repository.EntityFramework.DbFirstContext.SampleCRUD_Employee;
using Sample.CRUD.Repository.EntityFramework.GenericRepository;
using Sample.CRUD.Service.Interface;
using Sample.CRUD.Service;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Sample.CRUD.API.MinimalRoutes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Sample.CRUD.API.Extension
{
    internal static class AppConfigurationExtension
    {
        /// <summary>
        /// Registering all the services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void RegisterAllServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(ApplicationSettingsModel.ApplicationSettings).Get<ApplicationSettingsModel>();

            RegisterGeneralServices(services, configuration);
            RegisterJWTAuthentication(services, appSettings);
            RegisterUserServices(services);
            RegisterRepository(services, configuration, appSettings);

        }



        /// <summary>
        /// Adding all the middlewares
        /// </summary>
        /// <param name="app"></param>
        internal static void RegisterMiddleWares(WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<JwtAuthenticationExtension>();
            AddAuthenticationMiddleWare(app);
            app.UseHttpsRedirection();

        }


        internal static void RegisterRoutes(WebApplication app)
        {
            new EmployeeRoute(app.Logger).AddRoutes(app);
            new AuthenticationRoute(app.Logger).AddRoutes(app);
        }


        #region Categorized Services
        private static void RegisterGeneralServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample CRUD",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
            services.AddLogging();
            services.Configure<ApplicationSettingsModel>(configuration.GetSection(ApplicationSettingsModel.ApplicationSettings));
        }


        private static void RegisterUserServices(IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        private static void RegisterRepository(IServiceCollection services, IConfiguration configuration, ApplicationSettingsModel appSettings)
        {
            services.AddScoped(typeof(IBaseRepository), typeof(BaseRepository));
            services.AddDbContext<SampleCRUD_Employee_DbContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionString.SampleCRUD_Employee);
            });


        }


        private static void RegisterJWTAuthentication(IServiceCollection services, ApplicationSettingsModel appSetting)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSetting.Jwt.Issuer,
                    ValidAudience = appSetting.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.Jwt.SecretKey)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddAuthorization();

        }


        #endregion



        #region Custom Middlewares
        private static void AddAuthenticationMiddleWare(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        #endregion
    }
}
