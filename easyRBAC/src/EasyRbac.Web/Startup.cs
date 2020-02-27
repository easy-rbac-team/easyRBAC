using Autofac;
using EasyRbac.Application.User;
using EasyRbac.Dto.FluentValidate;
using EasyRbac.Dto.Mapper;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Utils.Denpendency;
using EasyRbac.Web.Options;
using EasyRbac.Web.WebExtentions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using SQLinq;
using SQLinq.Dialect;
using System.Data;
using System.Reflection;

namespace EasyRbac.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(
                option =>
                {
                    option.Filters.Add(new ModelVerifyFilter());
                }).AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());

                    })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserDtoVerify>());
            services.AddUtils(this.Configuration);
            services.UseDtoAutoMapper();
            services.AddCors();
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
            });


            services.AddAuthentication(
                option =>
                {
                    option.DefaultScheme = "token";
                    option.AddScheme<TokenAuthenticationHandler>("token", "token");
                });

            IConfiguration appConfiguration = this.Configuration.GetSection("app");
            services.Configure<AppOption>(appConfiguration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyRBAC API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin=>true)
                    .AllowAnyMethod();
                });
            });
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var sss = Assembly.Load("EasyRbac.DomainService");

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserService))).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("EasyRbac.Reponsitory"), sss).AsImplementedInterfaces();
            var connStr = this.Configuration.GetConnectionString("easyRBAc");
            builder.RegisterType<MySqlDialect>().As<ISqlDialect>().InstancePerLifetimeScope();
            builder.Register(c => new MySqlConnection(connStr)).As<IDbConnection>().InstancePerLifetimeScope();
            builder.RegisterType<MySqlIdGenerate>().As<IKeyedIdGenerate>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            //app.UseMvc();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }    
}
