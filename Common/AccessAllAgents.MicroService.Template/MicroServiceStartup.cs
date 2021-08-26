using AccessAllAgents.Logging;
using AccessAllAgents.MicroService.Common;
using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Environments;
using AccessAllAgents.MicroService.Template.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace AccessAllAgents.MicroService.Template
{
    public abstract class MicroServiceStartup<T> 
        where T : ICommonConfigService
    {
        protected MicroServiceStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opts =>
            {
                opts.Filters.Add<SerilogLoggingActionFilter>();
            });

            // Configure Compression level
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            // initialise and configure the configuration
            ConfigService = InitialiseConfigurations(services);


            
            // Add health checks
            services.AddHealthChecksUI();
            if (!AddHealthChecks(ConfigService, services))
            {
                // Add an empty health check
                services.AddHealthChecks();
            }

            // Add Response compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // configure jwt authentication
            services.AddAuthentication(o => { o.DefaultAuthenticateScheme = ApplicationConstants.IdentityApiKey; })
                .AddJwtBearer(ApplicationConstants.IdentityApiKey,
                    x =>
                    {
                        x.Audience = ApplicationConstants.Audience;
                        x.ClaimsIssuer = ApplicationConstants.Issuer;
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApplicationConstants.ClientSecret)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };

                        x.Events = new JwtBearerEvents
                        {
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(LoginTypes.Everyone, policy =>
                {
                    policy.RequireClaim(MicroServiceClaimTypes.LoginType, LoginTypes.Admin, LoginTypes.User, LoginTypes.Agency);
                });

                options.AddPolicy(LoginTypes.Admin, policy =>
                {
                    policy.RequireClaim(MicroServiceClaimTypes.LoginType, LoginTypes.Admin);
                });

                options.AddPolicy(LoginTypes.Agency, policy =>
                {
                    policy.RequireClaim(MicroServiceClaimTypes.LoginType, LoginTypes.Admin, LoginTypes.Agency);
                });

                options.AddPolicy(LoginTypes.User, policy =>
                {
                    policy.RequireClaim(MicroServiceClaimTypes.LoginType, LoginTypes.Admin, LoginTypes.User);
                });
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

            if (ControllerType != null)
            {
                services.AddMvc().AddApplicationPart(Assembly.GetAssembly(ControllerType)).AddControllersAsServices();
                services.AddControllers()
                    .AddControllersAsServices()
                    .AddNewtonsoftJson();
            }
        }

        // ReSharper disable once UnusedMember.Global (used by reflection)
        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IHttpContextAccessor, HttpContextAccessor>(new ContainerControlledLifetimeManager());

            var environment = Environment.GetEnvironmentVariable("SERVER_ENVIRONMENT");
            var serviceRegistrar = CreateServiceRegistrar(container);
            serviceRegistrar.RegisterServices(ConfigService);

            serviceRegistrar.InitialiseServices(environment).Wait();
            Container = container;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PreConfigure(app, env);

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }
            else
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
                // app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging(opts => {
                opts.EnrichDiagnosticContext = LogManager.EnrichFromRequest;
            });

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseResponseCompression();
            
            if (EnvironmentUtils.IsNuc())
            {
                app.UseCors(x =>
                    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger()
                .UseSwaggerUI(setup => { setup.SwaggerEndpoint("/swagger/v1/swagger.json", Documentation.ToString()); });


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

            PostConfigure(app, env);
        }

        protected abstract T InitialiseConfigurations(IServiceCollection services);

        protected abstract bool AddHealthChecks(T configService, IServiceCollection services);

        protected abstract void PreConfigure(IApplicationBuilder app, IWebHostEnvironment env);

        protected abstract void PostConfigure(IApplicationBuilder app, IWebHostEnvironment env);

        protected abstract IServiceRegistrar<T> CreateServiceRegistrar(IUnityContainer container);

        protected T ConfigService { get; private set; }

        protected abstract Type ControllerType { get; }

        protected abstract Documentations Documentation { get; }

        protected IUnityContainer Container { get; private set; }

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