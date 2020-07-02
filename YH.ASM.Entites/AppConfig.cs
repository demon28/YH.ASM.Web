using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace YH.ASM.Entites
{
    public class AppConfig
    {



        public AppConfig()
        {


        }



        public static string DB {

            get {

                var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string DB = config.GetSection("ConnectionStrings:DB").Value; // 分层键

                if (DB== "HomeDatabase")
                {
                    return HomeDatabase;
                }

                if (DB == "DevelopmentDatabase")
                {
                    return DevelopmentDatabase;
                }

                 return TestDatabase;
                

            }
        }


        private static string HomeDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:HomeDatabase").Value; 
                return conn;
            }

        }
        private static string DevelopmentDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:DevelopmentDatabase").Value;
                return conn;
            }

        }

        private static string TestDatabase
        {
            get
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string conn = config.GetSection("ConnectionStrings:TestDatabase").Value; 
                return conn;
            }

        }


        public static bool IgnoreAuthRight { get
            {

                var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

                var config = builder.Build();
                string conn = config.GetSection("IgnoreAuthRight").Value;
                return bool.Parse(conn);
            } }


        /// <summary>
        /// API加密Key 
        /// </summary>
        public static string ApiKey
        {
            get
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

                var config = builder.Build();
            
                string url = config.GetSection("Jwt:ApiKey").Value; // 分层键
                return url;
            }

        }

        /// <summary>
        /// jwt 接收者 
        /// </summary>
        public static string Audience
        {
            get
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string url = config.GetSection("Jwt:Audience").Value; // 分层键
                return url;
            }

        }
        /// <summary>
        /// jwt 签发者 
        /// </summary>
        public static string Issuer
        {
            get
            {
                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

                var config = builder.Build();

                string url = config.GetSection("Jwt:Issuer").Value; // 分层键
                return url;
            }

        }

    }
}
