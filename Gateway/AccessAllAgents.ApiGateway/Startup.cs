using AccessAllAgents.Logging;
using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Environments;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ocelot.Provider.Kubernetes;

// ReSharper disable UnusedMember.Global

namespace AccessAllAgents.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opts =>
            {
                opts.Filters.Add<SerilogLoggingActionFilter>();
            });

            services.AddHealthChecks()
                .AddCheck("Data Analysis API Gateway", () => HealthCheckResult.Healthy())
                .AddUrlGroup(new Uri(Configuration["URL_MS_LOGS"]), "Data Analysis Logs health status", tags: new[] {"logs"})
                .AddUrlGroup(new Uri(Configuration["URL_MS_USERS"]), "Data Analysis Users health status", tags: new[] {"users"})
                .AddUrlGroup(new Uri(Configuration["URL_MS_NOTIFICATIONS"]), "Data Analysis Notifications health status", tags: new[] { "notifications" })
                .AddUrlGroup(new Uri(Configuration["URL_MS_MESSAGING"]), "Data Analysis Messaging health status", tags: new[] { "messaging" })
                .AddUrlGroup(new Uri(Configuration["URL_MS_PROPERTY"]), "Data Analysis Property health status", tags: new[] { "property" });

            services.AddHealthChecksUI();
            if (EnvironmentUtils.IsNuc())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
            }

            // configure jwt authentication
            services.AddAuthentication(o => { o.DefaultAuthenticateScheme = ApplicationConstants.IdentityApiKey; })
                .AddJwtBearer(ApplicationConstants.IdentityApiKey, x =>
                {
                    x.Audience = ApplicationConstants.Audience;
                    x.ClaimsIssuer = ApplicationConstants.Issuer;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApplicationConstants.ClientSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/agentsChat")))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            // Add the access_token as a claim, as we may actually need it
                            if (context.SecurityToken is JwtSecurityToken accessToken)
                            {
                                if (context.Principal.Identity is ClaimsIdentity identity)
                                {
                                    identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Documentation.Version, new OpenApiInfo
                {
                    Title = Documentation.Title,
                    Version = Documentation.Version,
                    Description = Documentation.Description
                });
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddMvc();

            if (EnvironmentUtils.IsProduction())
            {
                services.AddOcelot(Configuration).AddKubernetes();
            }
            else
            {
                services.AddOcelot(Configuration);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging(opts => {
                opts.EnrichDiagnosticContext = LogManager.EnrichFromRequest;
            });

            if (EnvironmentUtils.IsNuc())
            {
                app.UseCors("CorsPolicy");
            }

            // app.UseHttpsRedirection();
            app.UseResponseCompression();
            
            app.UseSwagger().UseSwaggerUI(setup => { setup.SwaggerEndpoint("/swagger/v1/swagger.json", Documentation.ToString()); });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapHealthChecks("/health-status", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                config.MapHealthChecksUI(setup =>
                {
                    setup.UIPath = "/health";
                    setup.UseRelativeApiPath = true;
                });

                config.MapControllers();
            });

            app.UseOcelot().Wait();
        }

        public IConfiguration Configuration { get; }

        protected Documentations Documentation { get; } = new Documentations("Data Analysis API Gateway", "v1", "Application gateway for Data Analysis.");

        protected class Documentations
        {
            public Documentations(string title, string version, string description)
            {
                Title = title;
                Version = version;
                Description = description;
            }

            public string Title { get; }
            public string Version { get; }
            public string Description { get; }

            public override string ToString()
            {
                return $"{Title} ({Version})";
            }
        }
    }
}