using Boundaries.MessengerService.Handlers;
using Boundaries.MessengerService.Hubs;
using Boundary.Persistence.Contexts;
using Boundary.Persistence.Repositories;
using Core.Boundaries.MessengerService;
using Core.Boundaries.Persistence;
using Core.Models;
using Core.Rules;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ChatBot.Core;
using Webchat.Helpers;
using Webchat.Models;
using Webchat.Validators;

namespace Webchat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            services.AddDbContext<DefaultDbContext>((options) =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultDbConnection"),
                    (sqlServerDbContextOptionsBuilder) => sqlServerDbContextOptionsBuilder.MigrationsAssembly("Boundary.Persistence")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DefaultDbContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().AddFluentValidation();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(60);
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(30);
            }).AddJsonProtocol();

            ConfigureJwt(services);
            RegisterValidator(services);
            RegisterServicesScope(services);
            RegisterRepositoryScope(services);
            RegisterRulesScope(services);
            ConfigureRabbitMq(services);
        }

        private void ConfigureJwt(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
        }

        private void RegisterValidator(IServiceCollection services)
        {
            services.AddTransient<IValidator<SignUpRequest>, SignUpRequestValidator>();
            services.AddTransient<IValidator<AuthRequest>, SignInRequestValidator>();
        }

        private void RegisterServicesScope(IServiceCollection services)
        {
            services.AddScoped<JwtHandler>();

            services.AddSingleton<IMessageDelivery, MessageDelivery>();
            services.AddHostedService<MessageReceptor>();
        }

        private void RegisterRepositoryScope(IServiceCollection services) => services.AddScoped<IChatRoomRepository, ChatRoomRepository>();

        private void RegisterRulesScope(IServiceCollection services) => services.AddScoped<ChatRoomRules>();

        private void ConfigureRabbitMq(IServiceCollection services)
        {
            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<MessengerHub>("hub/messenger");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
