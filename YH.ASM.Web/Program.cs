using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace YH.ASM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateHostBuilder(args).Build().Run();
          // BuildWebHost(args).Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.AddLog4Net();//需要配置文件
            })
             .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                });

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
               .UseUrls("http://www.asm.cn:50428")
               .Build();


    }
}
