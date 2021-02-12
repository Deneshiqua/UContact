using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using UContact.MyReportApi.Database;
using UContact.MyReportApi.Database.Services;
using UContact.MyReportApi.Infrastructure;
using UContact.MyReportApi.Messaging;
using UContact.MyReportApi.Messaging.Receive;

namespace UContact.MyReportApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        public Startup(IConfiguration configuration,
            IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //configure the data protection system to persist keys to the specified directory
            services.AddDataProtection()
                .SetApplicationName(HostEnvironment.ApplicationName)
                .SetDefaultKeyLifetime(TimeSpan.FromDays(30))
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(HostEnvironment.ContentRootPath, "dataprotection")));

            //add response compression
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml" });
            });


            //all urls replace as lowercase
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            //add MyContactBb context
            string connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<MyReportDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            //add RabbitMq config
            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddScoped<IReportService, ReportService>();
            services.AddSingleton<IReportSender, ReportSender>();

            var mvcBuilder = services.AddControllers();


            //add newtonsoft json serializer
            mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.Formatting = HostEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //configure default json settings
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                Formatting = HostEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Will return the headers "api-supported-versions" and "api-deprecated-versions"
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            //versioning url segment
            services.AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true);

            //add mapster
            services.AddSingleton<IMapper, Mapper.MyMapster.Mapper>();

            //add swagger support for api docs
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(s => s.FullName);

                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = $"{HostEnvironment.ApplicationName}.xml";
                string xmlPath = Path.Combine(basePath, fileName);
                options.IncludeXmlComments(xmlPath);
            });

            //add newtonsoft json provider to swagger
            services.AddSwaggerGenNewtonsoftSupport();

            //add cors support
            services.AddCors();


            if (serviceClientSettings.Enabled)
            {
                // Rapor Oluþturma Dinleyicisi
                services.AddHostedService<ReportReceiver>();
                // Rapor Tamamlama Dinleyicisi
                services.AddHostedService<ReportGeneratedReceiver>();
            }


        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //initialize database
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //use forwarded header for reverse proxy
            app.UseForwardedHeaders();

            //use response compression
            app.UseResponseCompression();

            // use swagger
            app.UseSwagger();

            // use swagger middleware to serve swagger-ui
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;

                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                options.DefaultModelsExpandDepth(-1);
            });

            //use cors policy
            app.UseCors(x => x
                .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete)
                .AllowAnyOrigin()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<MyReportDbContext>())
            {
                try
                {
                    //apply migrations
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
