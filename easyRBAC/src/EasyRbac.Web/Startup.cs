using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using EasyRbac.Application.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EasyRbac.Dto.User;
using EasyRbac.Dto.FluentValidate;
using EasyRbac.Dto.Mapper;
using EasyRbac.Web.WebExtentions;
using EasyRbac.Utils.Denpendency;
using EasyRbac.Reponsitory.BaseRepository;
using EasyRbac.Reponsitory.Helper;
using EasyRbac.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SQLinq;
using SQLinq.Dialect;

namespace EasyRbac.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

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
                        options.SerializerSettings.Converters.Add(new LongToStringConverter());
                    })
                .AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<CreateUserDtoVerify>());
            services.AddUtils(this.Configuration);
            services.UseDtoAutoMapper();
            services.AddCors();

            //services.AddAuthentication(
            //    option =>
            //    {
            //        option.DefaultScheme = "test";
            //        option.DefaultAuthenticateScheme = "test";

            //        option.AddScheme<TokenAuthenticationHandler>("test", "test");
            //    });
            IConfiguration appConfiguration = this.Configuration.GetSection("app") ;
            services.Configure<AppOption>(appConfiguration);
            //services.AddSingleton<ISqlDialect, MySqlDialect>();
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

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserControllerService))).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("EasyRbac.Reponsitory"),sss).AsImplementedInterfaces();
            var connStr = this.Configuration.GetConnectionString("easyRBAc");
            builder.RegisterType<MySqlDialect>().As<ISqlDialect>().InstancePerLifetimeScope();
            builder.Register(c => new MySqlConnection(connStr)).As<IDbConnection>().InstancePerLifetimeScope();
            builder.RegisterType<MySqlIdGenerate>().As<IKeyedIdGenerate>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
            }

            app.UseCors(
                builder =>
                {
                    builder.WithOrigins("http://localhost:8010")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            

            app.UseMvc();
        }
    }
}
