using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FirstMvcCore
{
    public class Program
    {
        static Dictionary<string, string> appsettings = new Dictionary<string, string>()
        {
            {"AuthorName","Rabindra"},
            {"AuthorEmail","rabindras@hexaware.com"}
        };
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)            
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                //.ConfigureAppConfiguration(options=>
                //{
                //    options.SetBasePath(Directory.GetCurrentDirectory())
                //        .AddInMemoryCollection(appsettings)
                //        .AddXmlFile("mysettings.xml", optional: true)
                //        .AddIniFile("mysttings.ini", optional: true)
                //        .AddJsonFile("mysettings.json", optional: true)
                //        .AddKeyPerFile("FileDir", optional: true);
                //        //.AddEnvironmentVariables()
                //        //.AddCommandLine(args);
                //})
                .UseStartup<Startup>();
    }
}
