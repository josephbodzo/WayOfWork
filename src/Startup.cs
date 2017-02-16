using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using WayOfWork.AppCode.SwaggerFilters;

namespace WayOfWork
{
    public class Startup
    {
        private const string ApiName = "Way Of Work API";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = ApiName, Version = "v1"});
                var filePath = GetXmlCommentsPath();
                c.IncludeXmlComments(filePath);
                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteActions();

                c.OperationFilter<AuthorisationKeyHeaderOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // Add the swagger output. You can view the generated Swagger JSON at "/swagger/v1/swagger.json. Note the version number must match the one added in ConfigureServices above
            app.UseSwagger();

            // Add the swagger UI. Auto-generated, interactive docs at "/swagger
            app.UseSwaggerUi(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName + " V1"); });

            // Force a default load page when starting
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, Path.ChangeExtension(app.ApplicationName, "xml"));
        }
    }
}
