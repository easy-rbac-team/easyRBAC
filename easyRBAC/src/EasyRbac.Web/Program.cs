using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace EasyRbac.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin");
            var host = new WebHostBuilder()                
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .Build();
            Console.WriteLine("Run!");
            host.Run();
        }
    }
}
