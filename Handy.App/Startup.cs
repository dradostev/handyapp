using System;
using System.Text;
using AutoMapper;
using Handy.App.Configuration;
using Handy.App.Middlewares;
using Handy.App.Services;
using Handy.Bot.BotCommands;
using Handy.Bot.Core;
using Handy.Bot.HostedServices;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.ReminderContext.Entities;
using Handy.Domain.SharedContext.MappingProfiles;
using Handy.Domain.SharedContext.Services;
using Handy.Infrastructure;
using Handy.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Handy.App
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
            services.AddOptions();
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
            var jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>(); // костыль
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "localhost",
                        ValidateAudience = true,
                        ValidAudience = "localhost",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("UbeJoux01ULXUIfQkv")),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMediatR();
            services.AddDbContext<HandyDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"), 
                    n => n.MigrationsAssembly("Handy.Infrastructure")));

            services.AddSingleton(
                new MapperConfiguration(config => config.AddProfile(new MappingProfile())).CreateMapper()
            );
            
            // app services
            services.AddScoped<IAuthService, AuthService>();
            
            // repositories
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddScoped<IRepository<Note>, NoteRepository>();
            services.AddScoped<IRepository<Reminder>, ReminderRepository>();
            
            // telegram bot
            services.AddSingleton<HandyBot>();
            services.AddScoped<BotCommandExecutor>();
            services.AddScoped<IUpdateHandler, UpdateHandler>();
            services.AddHostedService<ReminderWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();

            app
                .ApplicationServices
                .GetService<HandyBot>()
                .Api
                .SetWebhookAsync(Environment.GetEnvironmentVariable("TELEGRAM_WEBHOOK_URL"))
                .Wait();
        }
    }
}