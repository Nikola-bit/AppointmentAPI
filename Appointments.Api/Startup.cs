using Appointments.Api.Data;
using Appointments.Api.Data.Interfaces;
using Appointments.Api.Data.Repositories;
using Appointments.Api.Filters;
using Appointments.Api.Models;
using Appointments.Api.Repositories.Classes;
using Appointments.Api.Repositories.Interfaces;
using Appointments.Api.Services;
using Appointments.Api.Services.Classes;
using Appointments.Api.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Appointments.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Appointments.Api.Repositories;

namespace Appointments.Api
{
    public class Startup
    {
        public static string TokenKey { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers(
                config =>
                {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
            );
            services.AddDbContext<AppointmentContext>(options => options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=Appointments;Integrated Security=true;"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AppointmentsAPI",
                    Description = "API used for our Organization",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "SkillUp",
                        Email = "contct@skillup.mk",
                        Url = new Uri("https://skillup.mk"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "No License",
                        Url = new Uri("https://example.com/license"),
                    }
                 });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, true);
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                //Enable Swagger authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token to authorize!",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Create Authorization header and insert the token in it
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
                });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomAtributeRepository, RoomAtributeRepository>();
            services.AddScoped<IRoomAtributeService, RoomAtributeService>();
            services.AddScoped<IPlatformConfigurationRepository, PlatformConfigurationRepository>();
            services.AddScoped<IPlatformConfigurationService, PlatformConfigurationService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingParticipantRepository, BookingParticipantRepository>();
            services.AddScoped<IBookingParticipantService, BookingParticipantService>();
            services.AddScoped<IBookingRecurrenceDaysRepository, BookingRecurrenceDaysRepository>();
            services.AddScoped<IBookingRecurrenceDaysService, BookingRecurrenceDaysService>();
            services.AddScoped<IBookingRecurrenceRepository, BookingRecurrenceRepository>();
            services.AddScoped<IBookingRecurrenceService, BookingRecurrenceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            TokenKey = Configuration.GetSection("AppSettings:TokenKey").Value;
            loggerFactory.AddFile("ErrorLogs/errorlog-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppointmentsAPI");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
