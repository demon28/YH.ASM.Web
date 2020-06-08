using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace YH.ASM.SSO
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
            services.AddControllersWithViews();
          

            services.AddIdentityServer(option =>
            {
                //可以通过此设置来指定登录路径，默认的登陆路径是/account/login
                option.UserInteraction.LoginUrl = "/Account/Login";

            })
      .AddDeveloperSigningCredential()
      .AddInMemoryApiResources(Config.GetApiResource())
      .AddInMemoryClients(Config.GetClients())
      .AddInMemoryIdentityResources(Config.GetIdentityResources());

            services.AddMvc();

            services.AddControllers()
        .AddNewtonsoftJson();

            services.AddCors(options => options.AddPolicy("Domain",
               builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

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
            }

            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseCors("Domain");
        }
    }
}
