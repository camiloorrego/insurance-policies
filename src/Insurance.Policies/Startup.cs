using AutoMapper;
using Insurance.Policies.Domain.Interfaces;
using Insurance.Policies.Domain.Services;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Infraestructure.Interfaces;
using Insurance.Policies.Infraestructure.Repositories;
using Insurance.Policies.Infraestructure.Repositories.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Insurance.Policies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Context
            services.AddScoped<IDb, Db>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IClientPolicyService, ClientPolicyService>();

            //Repos
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPolicyRepository, PolicyRepository>();
            services.AddScoped<IPolicyTypeRepository, PolicyTypeRepository>();
            services.AddScoped<IRiskTypeRepository, RiskTypeRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IClientPolicyRepository, ClientPolicyRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowCredentials();
            }));

            var key = Encoding.ASCII.GetBytes(Configuration["authSettings:key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Configuration["authSettings:validIssuer"],
                    ValidAudience = Configuration["authSettings:validAudience"]

                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async (ctx) =>
                    {
                        var db = ctx.HttpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>();
                        var user = ctx.Principal.FindFirst("UserId");
                        if (user != null)
                        {
                            db.Value.UserId = int.Parse(user.Value);
                        }
                    }
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDb db)
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

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            db.MigrateDataBase();

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
