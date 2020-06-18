using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YH.ASM.Entites;

namespace YH.ASM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //验证登录
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径
                o.LoginPath = new PathString("/Login/Index");

                //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
                o.AccessDeniedPath = new PathString("/Permission/NoPermission");

            });

            ////添加jwt验证：
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options => {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,//是否验证Issuer
            //            ValidateAudience = true,//是否验证Audience
            //            ValidateLifetime = true,//是否验证失效时间
            //            ClockSkew = TimeSpan.FromSeconds(30),
            //            ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //            ValidAudience = Entites.AppConfig.Audience,//Audience
            //            ValidIssuer = Entites.AppConfig.Issuer,//Issuer，这两项和前面签发jwt的设置一致
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.ApiKey))//拿到SecurityKey
            //        };
            //    });





            services.AddMvc().AddRazorRuntimeCompilation();

            services.AddControllers()
              .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
               });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
