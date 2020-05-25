using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace YH.ASM.Entites
{
    public class AppConfig
    {



        public AppConfig()
        {


        }


        public static string HomeDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:HomeDatabase").Value; // 分层键
                return conn;
            }

        }
        public static string DevelopmentDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:DevelopmentDatabase").Value; // 分层键
                return conn;
            }

        }

        public static string ProductDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:ProductDatabase").Value; // 分层键
                return conn;
            }

        }

    }
}
